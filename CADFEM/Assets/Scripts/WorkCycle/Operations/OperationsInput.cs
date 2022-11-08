using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ClassesForJsonDeserialize;

public class OperationsInput {
    private const string COMPLETED_STATUS_CODE = "COMPLETED";
    public  Operation[] Operations{ get; private set; }

    private readonly OperationsPanel _operationsPanel;
    private readonly OperationWebRequests _operationWebRequests;
    private readonly Queue<Operation> _operationsQueue;

    private readonly ControlParamsProcessing _controlParamsProcessing;
    private readonly HtmlTagsReplacer _htmlTagsReplacer;
    private bool AppendOperationIsNeeded => _workLog.is_fault_request && OperationQueueIsEmpty && !_workLog.is_new_operations_append;
    private bool OperationQueueIsEmpty => _operationsQueue.Count == 0;
    private bool OperationHasControlParams => _activeOperation.control_params_count > 0;
    private bool OperationPhotoRequired => _activeOperation.is_foto_required;

    private Operation _activeOperation;
    private WorkLogInfo _workLog;
    private int _faultWorkInstructionId;

    public OperationsInput(ThingWorksServices thingWorksServices, OperationsPanel operationPanel, ControlParamsInputPanel controlParamsInputPanel){
        _operationsPanel = operationPanel;
        _operationsPanel.SetImageDataGetter(thingWorksServices);
        
        _operationsQueue = new Queue<Operation>();

        _operationWebRequests = new OperationWebRequests(thingWorksServices);
        _controlParamsProcessing = new ControlParamsProcessing(controlParamsInputPanel, _operationWebRequests);

        _htmlTagsReplacer = new HtmlTagsReplacer();
    }

    public async UniTask<bool> Process(WorkLogInfo workLogInfo){
        await Initialize(workLogInfo);

        _operationsPanel.Open();

        while (!OperationQueueIsEmpty){
            _activeOperation = _operationsQueue.Peek();

            await _operationWebRequests.SetOperationPauseStatus(_activeOperation.id);
            _operationsPanel.SetOperation(_activeOperation);

            try{
                var processingResult = await Input();
                if (!processingResult) continue;
            }
            catch (OperationCanceledException oe){
                _operationsPanel.Close();
                return false;
            }
        }

        _operationsPanel.Close();
        return true;
    }

    private async UniTask<bool> Input(){
        await _operationsPanel.Process();

        if (OperationHasControlParams || OperationPhotoRequired){
            var controlParamsInputSuccess = await _controlParamsProcessing.Process(_activeOperation);
            if (!controlParamsInputSuccess) return false;
        }
        var checkParamsAndPhotoResult = await _operationWebRequests.CheckControlParamsAndPhoto(_activeOperation.id);

        if (checkParamsAndPhotoResult.is_check_ok){
            _activeOperation.operation_status_color = _activeOperation.COMPLETE_COLOR;
            await _operationWebRequests.SetOperationCompleteStatus(_activeOperation.id);
        }

        _operationsQueue.Dequeue();

        if (AppendOperationIsNeeded)
            await TransitionToNewOperations();
     
        return true;
    }

    private async UniTask TransitionToNewOperations(){
        await AddFaultOperations();
        _workLog.is_new_operations_append = true;
     
        await GetAndShowInstructionInfo(_faultWorkInstructionId);
        await OperationsInitialize();
    }

    private async UniTask Initialize(WorkLogInfo workLogInfo){
        _workLog = workLogInfo;
        await OperationsInitialize();
    }

    private async UniTask OperationsInitialize(){
        _operationsQueue.Clear();
        Operations = await _operationWebRequests.GetOperationsWithControlParams(_workLog.id);
        var completedColor = await _operationWebRequests.GetCompletedStatusColor();

        foreach (var operation in Operations){
            operation.COMPLETE_COLOR = completedColor;

            if (operation.operation_status_code != COMPLETED_STATUS_CODE)
                _operationsQueue.Enqueue(operation);
        }

        _operationsPanel.Initialize(Operations);
    }
    
    private async UniTask AddFaultOperations(){
        _faultWorkInstructionId = (await _operationWebRequests.GetInstructionFromSelectedCPInLastOperation(_workLog.id)).work_instruction_id;
        await _operationWebRequests.AppendOperationsFromWorkInstruction(_workLog.id, _faultWorkInstructionId);
    }

    private async UniTask GetAndShowInstructionInfo(int workInstructionId){
        var alertInfo = await _operationWebRequests.GetAdditionalInstructionInfo(workInstructionId);
        var safetyRules = _htmlTagsReplacer.ReplaceTagP(alertInfo.safety_rules);
        await WarningPopup.Instance.AwaitForAccept(safetyRules);
    }
}