using System.Collections.Generic;
using UnityEngine;

public class Arrow : Line {
    private const int DIRECTION_SPOT_POINT_INDEX = 6;

    private const float ARROWHEAD_LENGTH = 40f;
    private const float ARROWHEAD_WIDTH = 40f;

    public override void LineIsReady(){
        if (PositionsCount >= DIRECTION_SPOT_POINT_INDEX)
            ArrowheadDraw();
        base.LineIsReady();
    }

    private void ArrowheadDraw(){
        if (PositionsCount < DIRECTION_SPOT_POINT_INDEX) return;

        var endPos = GetPositionLast();
        var startPos = GetPos(PositionsCount - DIRECTION_SPOT_POINT_INDEX);

        var arrowheadPoints = CalculateArrowheadPoints(endPos, startPos);
        foreach (var point in arrowheadPoints)
            AddPoint(point);
    }

    private List<Vector2> CalculateArrowheadPoints(Vector3 endLinePoint, Vector2 startLinePoint){
        var points = new List<Vector2>();

        var dir = GetNormalizedLineDirection(endLinePoint, startLinePoint);
        var normal = GetNormalVector2D(dir);

        var arrowMiddlePoint = endLinePoint - dir * ARROWHEAD_LENGTH;
        var posLeft = arrowMiddlePoint + normal * ARROWHEAD_WIDTH;
        var posRight = arrowMiddlePoint - normal * ARROWHEAD_WIDTH;

        points.Add(posLeft);
        points.Add(endLinePoint);
        points.Add(posRight);

        return points;
    }

    private Vector3 GetNormalizedLineDirection(Vector2 endPos, Vector2 startPos) => (endPos - startPos).normalized;
    private Vector3 GetNormalVector2D(Vector2 vector) => new Vector3(-vector.y, vector.x);
}