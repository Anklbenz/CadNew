using System;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;

public class OperationsPreview {
    private readonly OperationsPanel _operationsPanel;
    private readonly ControlParamsPreviewPanel _controlParamsPreviewPanel;
    private Operation[] _operations;
    private Operation _operation;

    public OperationsPreview(ThingWorksServices thingWorksServices, OperationsPanel operationPanel, ControlParamsPreviewPanel controlParamsPreviewPanel){
        _operationsPanel = operationPanel;
        _controlParamsPreviewPanel = controlParamsPreviewPanel;
        //_controlParamsProcessing = new ControlParamsProcessing(controlParamsPanel, _operationWebRequests);
    }

    public async UniTask Process(Operation[] operations){
        Initialize(operations);

        _operationsPanel.Open();
        _operationsPanel.SetOperation(operations[0]);

        while (true){
            try{
                await _operationsPanel.Process();

                _controlParamsPreviewPanel.Open();
                await _controlParamsPreviewPanel.PreviewProcess(_operation);
                _controlParamsPreviewPanel.Close();
            }
            catch (OperationCanceledException oe){
                break;
            }
        }
        
        _operationsPanel.Close();
    }

    private void Initialize(Operation[] operations){
        _operations = operations;
        _operationsPanel.Initialize(_operations);

        foreach (var button in _operationsPanel.OperationButtons){
            button.OnClickEvent += ShowOperationData;
        }
    }

    private void ShowOperationData(Operation operation){
        _operation = operation;
        _operationsPanel.SetOperation(operation);
    }
}
