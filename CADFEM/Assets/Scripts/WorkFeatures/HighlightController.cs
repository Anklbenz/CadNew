using ClassesForJsonDeserialize;
using Enums;
using UnityEngine;

public class HighlightController {
    public bool Enabled{ get; set; }
    public HighlightState State{ get; private set; }
    
    private readonly HighlightStates _highlightStates;
    private readonly StateLayerMover _stateLayerMover;
    private readonly Transform[] _allHighlights;

    public HighlightController(Device device, StateLayerMover stateLayerMover){
        _highlightStates = device.HighlightStates;
       _stateLayerMover = stateLayerMover;
       _allHighlights = device.HighlightStates.AllHighlightsElements.ToArray();
    }

    public void RefreshData(TagValues tagValues){
        if(!Enabled) return;
        var state =  _highlightStates.GetState(tagValues);

        if (state == State) return;

        var needsToHighlight = _highlightStates.GetTransformsToHighlight(state);
        Highlight(needsToHighlight);
    }

    private void Highlight(Transform[] highlights){
        InactiveAll();
        _stateLayerMover.MoveToHighlight(highlights);
    }

    private void InactiveAll(){
        _stateLayerMover.MoveToInactive(_allHighlights);
    }

    public void SetToDefault(){
        _stateLayerMover.MoveToDefault(_allHighlights);
    }
}
