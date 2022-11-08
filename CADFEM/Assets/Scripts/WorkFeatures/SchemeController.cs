using ClassesForJsonDeserialize;
using UnityEngine;

public class SchemeController {
    public bool Enabled{ get; set; }

    private readonly SchemeDataIndicationController _schemeDataIndicationController;
    private readonly LookAtRotator _labelsLookAtRotator;
    private readonly Transform _schemeInstanceRect;

    private bool _lookAtEnabled;

    public SchemeController(Device device){
        _schemeInstanceRect = device.SchemeRectTransform;
        _schemeDataIndicationController = device.SchemeDataIndicationController;
        _labelsLookAtRotator = new LookAtRotator(device.LookAtCameraLabels);
    }

    public void SchemeVisible(bool state){
        _schemeInstanceRect.gameObject.SetActive(state);
    }

    public void LookAtSetActive(bool state){
        _lookAtEnabled = state;
        if (!state) _labelsLookAtRotator.LookToDefaultPosition();
    }

    public void RefreshData(TagValues tagValues){
        if (Enabled) _schemeDataIndicationController.Refresh(tagValues);
    }

    public void UpdateLookAt(){
        if (Enabled && _lookAtEnabled)
            _labelsLookAtRotator.LookAtCamera();
    }
}
