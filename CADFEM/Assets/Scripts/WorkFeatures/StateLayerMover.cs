using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateLayerMover : LayerMover {
  [SerializeField] private LayerMask defaultLayerMask, inactiveLayerMask, highlightLayerMask, transparentLayerMask;

  public void MoveToDefault(Transform[] transforms){
    SetLayer(transforms, defaultLayerMask);
  }
  
  public void MoveToInactive(Transform[] transforms){
    SetLayer(transforms, inactiveLayerMask);
  }

  public void MoveToHighlight(Transform[] transforms){
    SetLayer(transforms, highlightLayerMask);
  }

  public void MoveToTransparent(Transform[] transforms){
    SetLayer(transforms, transparentLayerMask);
  }
}
