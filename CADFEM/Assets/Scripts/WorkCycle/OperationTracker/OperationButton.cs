using System;
using ClassesForJsonDeserialize;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OperationButton : MonoBehaviour {
    [SerializeField] private TMP_Text idLabel;
    [SerializeField] private Button button;
    [SerializeField] private Image backImage;

    public event Action<Operation> OnClickEvent;
    
    public Operation Operation{ get; set; }

    public void Initialize(Operation operation){
        Operation = operation;
        idLabel.text = operation.mark_with_zeroes;
        
        UpdateColor();
    }

    public void UpdateColor(){
        if (ColorUtility.TryParseHtmlString(Operation.operation_status_color, out var color))
            backImage.color = color;
    }

    public void SetColor(Color color){
        backImage.color = color;
    }

    private void OnClicked(){
        OnClickEvent?.Invoke(Operation);
    }

    private void OnEnable() => button.onClick.AddListener(OnClicked);
    private void OnDestroy() => button.onClick.RemoveAllListeners();
}