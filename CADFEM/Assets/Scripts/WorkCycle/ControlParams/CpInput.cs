using System;
using System.Collections.Generic;
using System.Linq;
using ClassesForJsonDeserialize;
using RequestParamClasses;
using UnityEngine;

public class CpInput {
    public event Action DataChangedEvent;
    public bool IsParamsRequired => Operation.control_params_count > 0;
    public bool FieldsAreFilled => ControlParamsMenuItems.All(item => item.IsDataFilled);
    protected bool IsItemsListEmpty => ControlParamsMenuItems.Count == 0;
    protected readonly RectTransform ParamsPanelTransform;
    protected readonly List<ControlParamsMenuItem> ControlParamsMenuItems;
    protected readonly MenuItemFactory ItemFactory;
    protected Operation Operation;

    public CpInput(MenuItemFactory itemFactory, RectTransform paramsPanelTransform){
        ControlParamsMenuItems = new List<ControlParamsMenuItem>();
        ItemFactory = itemFactory;
        ParamsPanelTransform = paramsPanelTransform;
    }

    public ControlParamForSave[] GetData(){
        return ControlParamsMenuItems.Select(item => item.GetData).ToArray();
    }

    public virtual void Initialize(Operation operation){
        Operation = operation;

        if (!IsParamsRequired){
            ParamsPanelTransform.gameObject.SetActive(false);
            return;
        }

        ParamsPanelTransform.gameObject.SetActive(true);
        AddMenuItems();
    }

    protected virtual void AddMenuItems(){
        foreach (var controlParam in Operation.ControlParams){
            var itemMenu = ItemFactory.MenuItemCreate(controlParam.operation_cp_type_code);
            itemMenu.Initialize(controlParam);
            itemMenu.DataChangedEvent += DataChangedEvent;
            ControlParamsMenuItems.Add(itemMenu);
        }
    }

    public void ItemsClear(){
        foreach (var item in ControlParamsMenuItems){
            item.DataChangedEvent -= DataChangedEvent;
            item.Delete();
        }

        ControlParamsMenuItems.Clear();
    }
}
