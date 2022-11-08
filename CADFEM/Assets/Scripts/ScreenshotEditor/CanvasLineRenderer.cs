using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLineRenderer : UILineRenderer {

    public int PositionsCount => m_points.Count;
    public bool HasPoints => m_points.Count != 0;

    public float LineWidth{
        get => lineThickness;
        set => lineThickness = value;
    }

    public void AddPoint(){
        m_points.Add(new Vector2());
        SetAllDirty();
    }

    public void AddPoint(Vector2 position){
        m_points.Add(position);
        SetAllDirty();
    }

    public void SetPosition(int index, Vector2 position){
        m_points[index] = position;
        SetAllDirty();
    }

    public Vector2 GetPos(int index){
        return m_points[index];
    }

    public Vector2 GetPositionLast(){
        return m_points.Last();
    }
}
