using UnityEngine;


public class Line : CanvasLineRenderer {
    public void Initialize(Color lineColor, float lineWidth){
        color = lineColor;
        LineWidth = lineWidth;
    }

    public virtual void LineIsReady(){
        if (PositionsCount < 2)
            Destroy(gameObject);
    }
}
