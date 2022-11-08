using System;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using RequestParamClasses;

public class ControlParamsProcessing {

    private readonly SpriteToBase64Converter _spriteToBase64Converter;
    private readonly ControlParamsInputPanel _controlParamsInputPanel;
    private readonly OperationWebRequests _operationWebRequests;
    private UserEnteredParams _userEnteredParams;
    private Operation _operation;
    
    public ControlParamsProcessing(ControlParamsInputPanel controlParamsInputPanel, OperationWebRequests operationWebRequests ){
        _controlParamsInputPanel = controlParamsInputPanel;
        _operationWebRequests = operationWebRequests;
        _spriteToBase64Converter = new SpriteToBase64Converter();
    }

    public async UniTask<bool> Process(Operation operation){
        _operation = operation;
        var inputSuccess = await Input();
        if (!inputSuccess) return false;
     
        await Save();
        SaveForPreview();
        return true;
    }

    private async UniTask<bool> Input(){
        try{
            _controlParamsInputPanel.Open();
            _userEnteredParams = await _controlParamsInputPanel.Process(_operation);
            _controlParamsInputPanel.Close();
        }
        catch (OperationCanceledException oe){
            _controlParamsInputPanel.Close();
            return false;
        }

        return true;
    }

    private async UniTask Save(){
        await _operationWebRequests.SaveControlParams(_userEnteredParams.ControlParams);

        if (_userEnteredParams.Sprite != null){
            var base64String = _spriteToBase64Converter.SpriteToBase64(_userEnteredParams.Sprite);
            await _operationWebRequests.SavePhoto(_operation.id, base64String);
        }
    }

    private void SaveForPreview(){
        for (var i = 0; i < _userEnteredParams.ControlParams.Length; i++){
            var controlParam = _operation.ControlParams[i];
            var userEnteredData = _userEnteredParams.ControlParams[i];

            controlParam.state_fact = userEnteredData.state_fact ?? false;
            controlParam.value_fact = userEnteredData.value_fact ?? 0;
            controlParam.selected_id = userEnteredData.selected_id;
            controlParam.drop_down_selected = userEnteredData.selected_text;
        }

        _operation.Sprite = _userEnteredParams.Sprite;
    }
}
