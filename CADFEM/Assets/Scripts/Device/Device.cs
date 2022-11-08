using System;
using UnityEngine;

public class Device : MonoBehaviour {
    public Description description;
    [SerializeField] private Transforms transforms;
    [SerializeField] private SchemeDataIndicationController schemeDataIndicationControllerIndicationController;
    [SerializeField] private RectTransform[] lookAtCameraLabelsList;
    [SerializeField] private HighlightStates highlightStates;
 
    public Transforms Transforms => transforms;
    public RectTransform[] LookAtCameraLabels => lookAtCameraLabelsList;
    public HighlightStates HighlightStates => highlightStates;
    public SchemeDataIndicationController SchemeDataIndicationController => schemeDataIndicationControllerIndicationController;
    public RectTransform SchemeRectTransform => (RectTransform)schemeDataIndicationControllerIndicationController.transform;

    private void Awake(){
        highlightStates.Initialize();
    }
}

