using System;
using Enums;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class LineFactory {
     [SerializeField] private Line linePrefab;
     [SerializeField] private Arrow arrowPrefab;
     [SerializeField] private RectTransform parent;

     public Line GetBrush(BrushType type){
          return type switch
          {
               (BrushType.Line) => Get(linePrefab),
               (BrushType.Arrow) => Get(arrowPrefab),
               _ => Get(linePrefab)
          };
     }

     private T Get<T>(T prefab) where T : Line{
          return Object.Instantiate(prefab, parent);
     }
}
