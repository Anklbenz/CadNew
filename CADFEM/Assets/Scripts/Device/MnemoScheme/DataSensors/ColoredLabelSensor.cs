using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColoredLabelSensor : LabelSensor {
    [SerializeField] private Image backgroundImage;
   
    public void UpdateData(string label, string fillColor){
        base.UpdateData(label);
        if (ColorUtility.TryParseHtmlString(fillColor, out var color))
            backgroundImage.color = color;
    }
}
