using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitter {
    public Vector2 FitRect(Vector2 rectSize, Vector2 parentRectSize){
        float width, height;

        var parentAspectRatio = parentRectSize.x / parentRectSize.y;
        var clipAspectRatio = rectSize.x / rectSize.y;

        if (parentAspectRatio < clipAspectRatio){
            width = (int)parentRectSize.x;
            height = width / clipAspectRatio;
        }
        else{
            height = (int)parentRectSize.y;
            width = height * clipAspectRatio;
        }

        return new Vector2(width, height);
    }
}
