using UnityEngine;

public class ModelVisibilityController  {
    private readonly Transform[] _outerBoxChildRenderers, _doorChildRenders;
    private readonly Transform  _modelTransform, _outerBoxTransform;
    private readonly StateLayerMover _stateLayerMover;
    
    public ModelVisibilityController(Device device,  StateLayerMover stateLayerMover){
        _stateLayerMover = stateLayerMover;

        _modelTransform = device.Transforms.modelParent;
        _outerBoxTransform = device.Transforms.outerBoxParent;

        _doorChildRenders = device.Transforms.doorParent.GetComponentsInChildren<Transform>();
        _outerBoxChildRenderers = _outerBoxTransform.GetComponentsInChildren<Transform>();
    }
  
    public void ModelVisible(bool state){
        _modelTransform.gameObject.SetActive(state);
    }
    public void OuterBoxVisible(bool state){
        _outerBoxTransform.gameObject.SetActive(!state);
    }
    
    public void DoorTransparent(bool state){
        if (state)
            _stateLayerMover.MoveToTransparent(_doorChildRenders);
        else
            _stateLayerMover.MoveToDefault(_doorChildRenders);
    }
}

