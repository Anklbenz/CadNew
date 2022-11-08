using System.Collections.Generic;
using ClassesForJsonDeserialize;
using RequestParamClasses;
using TMPro;
using UnityEngine;

public class MenuItemDropDown : ControlParamsMenuItem {
    [SerializeField] private TMP_Dropdown dropdownInput;
    public override bool IsDataFilled => _dataIsSelected;
    public override bool ReadOnly{
        get => !dropdownInput.interactable;
        set => dropdownInput.interactable = !value;
    }

    private readonly List<int> _dropdownRowsValues = new();
    private bool _dataIsSelected;

    public override void Initialize(ControlParam controlParam){
        base.Initialize(controlParam);
        DropDownItemsAdd(controlParam.DropDownRows);
    }

    public override void SetData(ControlParam data){
        dropdownInput.value = data.selected_id;
    }

    private void DropDownItemsAdd(WorkLogOperationDropDownRow[] rows){
        foreach (var dropdownRow in rows){
            var rowId = dropdownRow.id;
            var rowName = dropdownRow.name;
            dropdownInput.options.Add(new TMP_Dropdown.OptionData($"{rowId} - {rowName}"));

            _dropdownRowsValues.Add(rowId);
        }
        dropdownInput.SetValueWithoutNotify(-1);
    }

    private void OnValueChanged(int index){
        _dataIsSelected = true;
        Data.selected_id = _dropdownRowsValues[index]; // dropdownInput.options[index].text
        Data.selected_text = dropdownInput.options[index].text;
        
        DataChangedEvent?.Invoke();
    }
    
    private void OnEnable(){
        dropdownInput.onValueChanged.AddListener(OnValueChanged);
        dropdownInput.onValueChanged.AddListener(delegate { HighlightDataBackground(); });
    }

    private void OnDisable() => dropdownInput.onValueChanged.RemoveListener(OnValueChanged);
}