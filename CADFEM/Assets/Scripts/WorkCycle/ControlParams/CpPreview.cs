using ClassesForJsonDeserialize;
using UnityEngine;

public class CpPreview : CpInput{
    public CpPreview(MenuItemFactory itemFactory, RectTransform paramsPanelTransform) : base(itemFactory, paramsPanelTransform){ }

    public override void Initialize(Operation operation){
        Operation = operation;

        if (IsParamsRequired){
            ParamsPanelTransform.gameObject.SetActive(true);
            if (!IsItemsListEmpty) ItemsClear();
            AddMenuItems();
            return;
        }

        ParamsPanelTransform.gameObject.SetActive(false);
    }

    protected override void AddMenuItems(){
        foreach (var controlParam in Operation.ControlParams){
            var itemMenu = ItemFactory.MenuItemCreate(controlParam.operation_cp_type_code);
            itemMenu.Initialize(controlParam);
            itemMenu.SetData(controlParam);
            itemMenu.ReadOnly = true;
            ControlParamsMenuItems.Add(itemMenu);
        }
    }
}
