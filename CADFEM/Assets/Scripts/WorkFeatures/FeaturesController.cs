using System;

public class FeaturesController : IDisposable {
    private readonly ModelVisibilityController _modelVisibilityController;
    private readonly HighlightController _highlightController;
    private readonly SchemeController _schemeController;

    private readonly AssetDataReceiver _assetDataReceiver;
    private readonly SettingsMenuControl _settingsMenuControl;

    public FeaturesController(SettingsMenuControl settingsMenuControl, Device device, StateLayerMover stateLayerMover, AssetDataReceiver assetDataReceiver){
        _assetDataReceiver = assetDataReceiver;
        
        _modelVisibilityController = new ModelVisibilityController(device, stateLayerMover);
        _highlightController = new HighlightController(device, stateLayerMover);
        _schemeController = new SchemeController(device);

        _settingsMenuControl = settingsMenuControl;
        
        _settingsMenuControl.OnModelVisibleTurnEvent += OnModelVisibleTurn;
        _settingsMenuControl.OnFilingViewTurnEvent += OnFilingViewTurn;
        _settingsMenuControl.OnHighlightTurnEvent += OnHighlightTurn;
        _settingsMenuControl.OnSchemeViewTurnEvent += OnSchemeViewTurn;
        _settingsMenuControl.OnLookAtSwitchedEvent += OnLookAtSwitched;
        
        _assetDataReceiver.OnTagDataReceivedEvent += OnTagsDataReceived;
    }

    private void OnModelVisibleTurn(bool state){
        _modelVisibilityController.ModelVisible(state);
    }
    private void OnFilingViewTurn(bool state){
        _modelVisibilityController.OuterBoxVisible(!state);
    }
    
    private void OnHighlightTurn(bool state){
        _highlightController.Enabled = state;
        if(!state) _highlightController.SetToDefault();
    }

    private void OnSchemeViewTurn(bool state){
       _schemeController.Enabled = state;
       _schemeController.SchemeVisible(state);
        _modelVisibilityController.DoorTransparent( state);
    }

    private void OnLookAtSwitched(bool state){
        _schemeController.LookAtSetActive(state);
    }

    private void OnTagsDataReceived(){
        _schemeController.RefreshData(_assetDataReceiver.TagValues);
        _highlightController.RefreshData(_assetDataReceiver.TagValues);
    }

    public void Update(){
       _schemeController.UpdateLookAt();
    }
    
    public void Dispose(){
        _settingsMenuControl.OnModelVisibleTurnEvent -= OnModelVisibleTurn;
        _settingsMenuControl.OnFilingViewTurnEvent -= OnFilingViewTurn;
        _settingsMenuControl.OnHighlightTurnEvent -= OnHighlightTurn;
        _settingsMenuControl.OnSchemeViewTurnEvent -= OnSchemeViewTurn;
        _settingsMenuControl.OnLookAtSwitchedEvent -= OnLookAtSwitched;
        
        _assetDataReceiver.OnTagDataReceivedEvent -= OnTagsDataReceived;
    }
}