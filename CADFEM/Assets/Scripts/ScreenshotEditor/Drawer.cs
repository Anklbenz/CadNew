using UnityEngine;
[System.Serializable]
public class Drawer {
    [SerializeField] private RectTransform drawingRect;
    public Line Line{ get; set; }

    public void Draw(Vector2 mousePos){
        if(Line ==null) return;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingRect, mousePos, null, out var point)) return;
        if (Line.HasPoints && IsDuplicate(point)) return;

        Line.AddPoint(point);
    }

    public void StopDraw(){
        Line.LineIsReady();
        Line = null;
    }

    private bool IsDuplicate(Vector2 position){
        return position == Line.GetPositionLast();
    }
}
