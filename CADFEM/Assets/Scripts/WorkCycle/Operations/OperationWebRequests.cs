using System.Linq;
using Cysharp.Threading.Tasks;
using ClassesForJsonDeserialize;
using RequestParamClasses;
using UnityEngine;

public class OperationWebRequests {
    private const string CP_DROP_DOWN_CODE = "CP_DROP_DOWN";
    private const string STATUS_COMPLETED = "COMPLETED";

    private readonly ThingWorksServices _thingWorksServices;

    public OperationWebRequests(ThingWorksServices thingWorksServices){
        _thingWorksServices = thingWorksServices;
    }

    public async UniTask<string> GetCompletedStatusColor(){
        var statusesColor = await GetOperationStatusesColors();
        return (statusesColor.rows.First(p => p.code == STATUS_COMPLETED)).color;
    } 
    public async UniTask<OperationStatusesArray> GetOperationStatusesColors(){
        return await _thingWorksServices.GetOperationStatuses();
    }

    public async UniTask SavePhoto(int workLogOperationId, string base64String){
        await _thingWorksServices.WorkLogOperationSaveFoto(workLogOperationId, base64String);
    }

    public async UniTask SaveComment(int workLogId, string comment){
        if (comment != string.Empty)
            await _thingWorksServices.WorkLogSaveComment(workLogId, comment);
    }

    public async UniTask<SavePhotoStatus> CheckControlParamsAndPhoto(int activeOperationId){
        return await _thingWorksServices.WorkLogOperationCheckControlParamsAndFoto(activeOperationId);
    }

    public async UniTask<SelectedCPFromLastOperation> GetInstructionFromSelectedCPInLastOperation(int workLogId){
        return  await _thingWorksServices.WorkLogGetSelectedCPFromLastOperation(workLogId);
    }

    public async UniTask<WorkInstructionInfo> GetAdditionalInstructionInfo(int workInstructionId){
        return await _thingWorksServices.WorkInstructionGetInfo(workInstructionId);
    }
    
    public async UniTask AppendOperationsFromWorkInstruction(int workLogId, int workInstructionId){
        await _thingWorksServices.WorkLogAppendOperationsFromWorkInstruction(workLogId, workInstructionId);
    }

    public async UniTask SetOperationPauseStatus(int operationId){
        await _thingWorksServices.WorkLogOperationSetPaused(operationId);
    }
    public async UniTask SetOperationCompleteStatus(int operationId){
        await _thingWorksServices.WorkLogOperationSetCompleted(operationId);
    }
    public  async UniTask SaveControlParams(ControlParamForSave[] saveParams){
        foreach (var saveParam in saveParams)
            await _thingWorksServices.WorkLogOperationSaveControlParam(saveParam);
    }

    public async UniTask<Operation[]> GetOperationsWithControlParams(int workLogId){
        var operations = await _thingWorksServices.WorkLogGetOperations(workLogId);
        foreach (var operation in operations){
           if (operation.control_params_count > 0)
                await InitializeControlParams(operation);
        }

        return operations;
    }

    private async UniTask InitializeControlParams(Operation operation){
        operation.ControlParams = await _thingWorksServices.WorkLogOperationGetControlParams(operation.id);
        if (operation.ControlParams != null)
            await InitializeDropDown(operation.ControlParams);
    }

    private async UniTask InitializeDropDown(ControlParam[] controlParams){
        foreach (var controlParam in controlParams)
            if (controlParam.operation_cp_type_code == CP_DROP_DOWN_CODE)
                controlParam.DropDownRows = await _thingWorksServices.WorkLogOperationGetDropDownRowsForControlParam(controlParam.id);
    }
}