using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UiVisibleController {

    private readonly HUDControls _hudControls;
    private readonly OperationsPanel _operationsPanel;
    private readonly ControlParamsInputPanel _controlParamsInputPanel;
    public static UiVisibleController Instance{ get; private set; }

    public UiVisibleController(HUDControls hudControls, OperationsPanel operationsPanel, ControlParamsInputPanel controlParamsInputPanel){
        _hudControls = hudControls;
        _operationsPanel = operationsPanel;
        _controlParamsInputPanel = controlParamsInputPanel;
        Instance = this;
    }

    public void HideAll(){
        _hudControls.Hide();
        _operationsPanel.Hide();
        _controlParamsInputPanel.Hide();
    }
    
    public void ShowAll(){
        _hudControls.Show();
        _operationsPanel.Show();
        _controlParamsInputPanel.Show();
    }

}