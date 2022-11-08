using UnityEngine;
using RequestParamClasses;

public class ProcessBootstrap : MonoBehaviour {
    private const string LOGIN = "Basic QWRtaW5pc3RyYXRvcjoxMjM0NXF3ZXJ0YXNkZkc=";
    private const string URL = "https://svtaneko-migration.digitaltwin.ru:8443";
    private const string SERVICES_URL = "/Thingworx/Things/ServiceVizor.VuforiaAR_TH/Services/";
    
    [SerializeField] private TransferObject transferObject;
    [SerializeField] private Transform creationPoint;

    [Header("TasksSelect")]
    [SerializeField] private TasksSelectPanel tasksSelectPanel;

    [Header("Popups")] // в один класс все попапы
    [SerializeField] private AlertPopup alertPopup;

    [SerializeField] private CommentPopup commentPopup;
    [SerializeField] private WarningPopup warningPopup;

    [Header("OperationsInput")]
    [SerializeField] private OperationsPanel operationsInputPanel;

    [SerializeField] private ControlParamsInputPanel paramsInputPanel;

    [Header("OperationsPreview")]
    [SerializeField] private OperationsPanel operationsPreviewPanel;

    [SerializeField] private ControlParamsPreviewPanel paramsPreviewPanel;

    [Header("Misc")]
    [SerializeField] private HUDControls hudControls;

    [SerializeField] private SettingsMenuControl settingsMenuControl;
    [SerializeField] private TasksOpenButton tasksOpenButton;

    [Header("ActiveLayers")]
    [SerializeField] private StateLayerMover stateLayerMover;

    private string AssetName => _device.description.AssetSystemName;
    private UserInfo _userInfo;
    private UiVisibleController _uiVisibleController;

    private ThingWorksServices _thingWorksServices;
    private FeaturesController _featuresController;
    private TasksSelector _tasksSelector;
    private WorkCycle _workCycle;

    private AssetDataReceiver _assetDataReceiver;
    private Device _device;
    private readonly DeviceCreator _deviceCreator = new();

    private void Start(){
        _device = _deviceCreator.Create(transferObject.prefab, creationPoint);

        _thingWorksServices = new ThingWorksServices(URL, SERVICES_URL, LOGIN);
        _thingWorksServices.OnServerResponseEvent += RequestResultLogger;
 
        _assetDataReceiver = new AssetDataReceiver(_thingWorksServices);
        _tasksSelector = new TasksSelector(_thingWorksServices, tasksSelectPanel);

        var operationsShow = new OperationsPreview(_thingWorksServices, operationsPreviewPanel, paramsPreviewPanel);
        var operationsInput = new OperationsInput(_thingWorksServices, operationsInputPanel, paramsInputPanel);
        _workCycle = new WorkCycle(_thingWorksServices, _tasksSelector, operationsInput, operationsShow);
        _workCycle.SetAssetName(AssetName);
        
        _uiVisibleController = new UiVisibleController(hudControls, operationsInputPanel, paramsInputPanel);
        _featuresController = new FeaturesController(settingsMenuControl, _device, stateLayerMover, _assetDataReceiver);

        alertPopup.Initialize();
        commentPopup.Initialize();
        warningPopup.Initialize();

        _assetDataReceiver.OnTasksAvailableEvent += TaskButtonVisible;
        tasksOpenButton.SetPosition(_device.Transforms.buttonTransform.position);
        tasksOpenButton.button.onClick.AddListener(OpenTaskSelectWindow);

        SetUserInfoAndAssentNameData();
    }

    private async void SetUserInfoAndAssentNameData(){
        var userInfo = await _thingWorksServices.GetUserInfo();
        hudControls.SetUserInfoAndAssetName(userInfo.username, AssetName);
    }

    private void OpenTaskSelectWindow(){
        _workCycle.TasksSelection();
    }

    private void TaskButtonVisible(bool state){
        tasksOpenButton.Visible(state);
    }

    //создать класс получающий данные по таймеру
    private async void Update(){
        _featuresController.Update();

        if (Input.GetKeyDown(KeyCode.X)){
            await _assetDataReceiver.GetAvailableTasks(_device.description.AssetSystemName);
            await _assetDataReceiver.GetSensorsData(_device.description.AssetSystemName);
        }
    }

    private void RequestResultLogger(string responseCode){
        Debug.Log(responseCode);
    }
}