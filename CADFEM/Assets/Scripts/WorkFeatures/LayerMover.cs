using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMover {
    protected void SetLayer(Transform[] transforms, LayerMask layerMask){
        foreach (var line in transforms)
            line.gameObject.layer =(int) Mathf.Log(layerMask.value, 2);;
    }
}
