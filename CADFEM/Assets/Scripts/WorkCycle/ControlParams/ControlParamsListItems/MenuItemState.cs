using ClassesForJsonDeserialize;
using RequestParamClasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemState : ControlParamsMenuItem {
    [SerializeField] private TMP_Text nominalText;
    [SerializeField] private Toggle toggleInput, toggleValueNominal;

    public override bool IsDataFilled => Data.state_fact != null;

    public override bool ReadOnly{
        get => !toggleInput.interactable;
        set => toggleInput.interactable = !value;
    }

    public override void Initialize(ControlParam controlParam){
        base.Initialize(controlParam);
        nominalText.text = NOMINAL_VALUE_TEXT;
        toggleValueNominal.isOn = controlParam.state_nominal;
    }

    public override void SetData(ControlParam data){
         toggleInput.isOn = data.state_fact;
    }

    private void OnValueChanged(bool state){
        Data.state_fact = state;
        DataChangedEvent?.Invoke();
    }

    private void OnEnable(){
        toggleInput.onValueChanged.AddListener(OnValueChanged);
        toggleInput.onValueChanged.AddListener(delegate { HighlightDataBackground(); });
    }

    private void OnDisable(){
        toggleInput.onValueChanged.RemoveAllListeners();

    }
}
