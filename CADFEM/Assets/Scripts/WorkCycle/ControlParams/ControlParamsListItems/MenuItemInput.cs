using System;
using ClassesForJsonDeserialize;
using RequestParamClasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemInput : ControlParamsMenuItem {
    [SerializeField] private TMP_InputField valueInput;
    [SerializeField] protected TMP_Text nominalText, unitsText;

    public override bool IsDataFilled => Data.value_fact != null;
    public override bool ReadOnly{
        get => !valueInput.interactable;
        set => valueInput.interactable = !value;
    }

    public override void Initialize(ControlParam controlParam){
        base.Initialize(controlParam);
        nominalText.text = $"{NOMINAL_VALUE_TEXT} \n {controlParam.value_nominal}  {controlParam.value_unit}";
        unitsText.text = controlParam.value_unit;
    }

    public override void SetData(ControlParam data){
        valueInput.text = data.value_fact.ToString();
    }

    private void OnValueChanged(string text){
        if (float.TryParse(text, out var value)){
            Data.value_fact = value;
            DataChangedEvent?.Invoke();
        }
        else{
            Data.value_fact = null;
        }
    }

    private void OnEnable(){
        valueInput.onValueChanged.AddListener(OnValueChanged);
        valueInput.onValueChanged.AddListener(delegate { HighlightDataBackground(); });
    }

    private void OnDisable(){
        valueInput.onValueChanged.RemoveAllListeners();
    }
}
