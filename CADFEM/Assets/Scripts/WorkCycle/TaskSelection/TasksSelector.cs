using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using ClassesForJsonDeserialize;
using RequestParamClasses;

public class TasksSelector {
    private const int OPERATION_CANCELED_CODE = -1;

    private const string REQUEST_TASK_TYPE = "REQUEST";
    private const string WORK_LOG_CREATE_MSG = "Создать обход на основании заявки";
    private const string WORK_LOG_EXECUTE_MSG_1 = "На основании заявки:";
    private const string WORK_LOG_EXECUTE_MSG_2 = "Успешно создан обход:";
    private const string WORK_LOG_EXECUTE_MSG_3 = "Приступить к выполнению?";
    private bool SelectedOperationTypeIsRequest => _selectedOperationData.row_type_code == REQUEST_TASK_TYPE;

    private readonly ThingWorksServices _thingWorksServices;
    private readonly TasksSelectPanel _tasksSelectPanel;

    private TaskData _selectedOperationData;
    private CreatedWorkLog _createdWorkLogData;
    private int _workLogId;

    public TasksSelector(ThingWorksServices thingWorksServices, TasksSelectPanel tasksSelectPanel){
        _thingWorksServices = thingWorksServices;
        _tasksSelectPanel = tasksSelectPanel;

        GetUserInfo();
    }

    public async UniTask<int> TaskSelectionProcess(string assetName){
        var operations = await _thingWorksServices.RequestAndWorkLogGetView(assetName);
        _tasksSelectPanel.Initialize(operations);
        _tasksSelectPanel.Open();

        try{
            while (true){
                _selectedOperationData = await _tasksSelectPanel.TaskSelectionProcess();
                _workLogId = _selectedOperationData.work_log_id;
                if (SelectedOperationTypeIsRequest){
                    //Dialog: WorkLog create
                    var createWokLogFromRequestDecision = await AlertPopup.Instance.AwaitForDecision($"{WORK_LOG_CREATE_MSG} \n{_selectedOperationData.row_code}-{_selectedOperationData.row_comment}?");
                    if (!createWokLogFromRequestDecision) continue;
                    _createdWorkLogData = await _thingWorksServices.WorkLogCreateFromRequest(_selectedOperationData.request_id);
                    _workLogId = _createdWorkLogData.id;
                    //Dialog: start working on WorkLog
                    var performingWorLogDecision = await AlertPopup.Instance.AwaitForDecision($"{WORK_LOG_EXECUTE_MSG_1}\n \"{_selectedOperationData.row_code}-{_selectedOperationData.row_comment}\" \n {WORK_LOG_EXECUTE_MSG_2}\n \"{_createdWorkLogData.code}\" \n {WORK_LOG_EXECUTE_MSG_3}");
                    if (performingWorLogDecision) break;

                    continue;
                }

                break;
            }
        }

        catch (OperationCanceledException oe){
            _workLogId = OPERATION_CANCELED_CODE;
        }

        _tasksSelectPanel.Close();
        return _workLogId;
    }

    private async void GetUserInfo(){
        var userInfo = await _thingWorksServices.GetUserInfo();
        _tasksSelectPanel.SetUserInfo($"{userInfo.username} {userInfo.full_name} ");
    }
}