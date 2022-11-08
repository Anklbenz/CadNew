using ClassesForJsonDeserialize;
using UnityEngine;
using UnityEngine.UI;

public class OperationsInputPanel : OperationsPanel {
    [SerializeField] private Image acceptButtonImage;
    
    private readonly Color OperationHasParamsButtonColor = new Color(1, 0, 0);
    private readonly Color OperationWithoutParamsButtonColor = new Color(0.26f, 0.62f, 0.39f);
    private bool OperationHasParamOrPhoto => Operation.is_foto_required || Operation.control_params_count > 0;
    public override void SetOperation(Operation operation){
        base.SetOperation(operation);
        SetAcceptButtonColor();
    }
    
    private void SetAcceptButtonColor(){
        acceptButtonImage.color = OperationHasParamOrPhoto ? OperationHasParamsButtonColor : OperationWithoutParamsButtonColor;
    }
}