using System;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WorkCycle {
    private string SUCCESS_TASK_HEADER = "Обход завершен.";

    private string CancelTaskHeader => $"Прекратить выполнение обхода \"{_workLogInfo.code}\" ?";
    private string CancelTaskCommentPlaceholder => $"Введите причину прекращения обхода \"{_workLogInfo.code}\" (необязательно) ";

    private string GoToPreviewMessage => $"Обход \"{_workLogInfo.code}\" завершен. Перейти в режим ознакомления?";
    private string FinishPreviewMessage => $"Завершить ознакомление с результатами обхода \"{_workLogInfo.code}\"?";
    private string SuccessTaskCommentPlaceholder => $"Оставить коментрий к обходу \"{_workLogInfo.code}\" (необязательно)";

    private readonly ThingWorksServices _thingWorksServices;
    private readonly TasksSelector _tasksSelector;
    private readonly OperationsInput _operationsInput;
    private readonly OperationsPreview _operationsPreview;

    private WorkLogInfo _workLogInfo;
    private string _assetName;

    public WorkCycle(ThingWorksServices thingWorksServices, TasksSelector tasksSelector, OperationsInput operationsInput, OperationsPreview operationPreview){
        _thingWorksServices = thingWorksServices;
        _tasksSelector = tasksSelector;
        _operationsInput = operationsInput;
        _operationsPreview = operationPreview;
    }

    public void SetAssetName(string assetName){
        _assetName = assetName;
    }

    public async void TasksSelection(){
        // return "-1" if canceled
        var workLogId = await _tasksSelector.TaskSelectionProcess(_assetName);

        if (workLogId >= 0)
            GetOperations(workLogId);
    }

    private async void GetOperations(int workLogId){
        _workLogInfo = await _thingWorksServices.WorkLogGetInfo(workLogId);
        OperationPerforming(_workLogInfo);
    }

    private async void OperationPerforming(WorkLogInfo workLogInfo){
        var operationInput = await OperationsInput(workLogInfo);
        
        if (operationInput){
            var goToPreviewModeDecision = await AlertPopup.Instance.AwaitForDecision(GoToPreviewMessage);

            if (goToPreviewModeDecision)
                await OperationPreview(_operationsInput.Operations);


            var taskComment = await CommentPopup.Instance.AwaitForDecisionWithComment(SUCCESS_TASK_HEADER, SuccessTaskCommentPlaceholder);
            await SaveComment(taskComment);
            return;
        }

        var cancelComment = await CommentPopup.Instance.AwaitForDecisionWithComment(CancelTaskHeader, CancelTaskCommentPlaceholder);
        await SaveComment(cancelComment);

    }

    private async UniTask<bool> OperationsInput(WorkLogInfo workLogInfo){
        while (true){
            var inputSuccess = await _operationsInput.Process(workLogInfo);
            if (inputSuccess){
                return true;
            }

            var cancelTask = await AlertPopup.Instance.AwaitForDecision(CancelTaskHeader);
            if (cancelTask)
                return false;
        }
    }

    private async UniTask OperationPreview(Operation[] operation){

        while (true){
            await _operationsPreview.Process(operation);
            var finishPreviewDecision = await AlertPopup.Instance.AwaitForDecision(FinishPreviewMessage);

            if (finishPreviewDecision) break;
        }
    }

    private async UniTask SaveComment(string taskComment){
        if (taskComment != string.Empty)
            await _thingWorksServices.WorkLogSaveComment(_workLogInfo.id, taskComment);
    }
}