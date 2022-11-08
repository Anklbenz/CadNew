using System.Globalization;
using ClassesForJsonDeserialize;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagValueSensor : MonoBehaviour {
    [SerializeField] private TMP_Text tagIdText, tagIdFirstSymbolText, valueText, unitsText;
    [SerializeField] private Image valueBackground, isConnectedImage;

    public void UpdateData(TagValue data, bool isConnected){
        tagIdText.text = data.tag;
        valueText.text = data.value.ToString(CultureInfo.InvariantCulture);
        unitsText.text = data.unit;

        if (ColorUtility.TryParseHtmlString(data.fill, out var color))
            valueBackground.color = color;

        if (data.tag.Length > 0)
            tagIdFirstSymbolText.text = data.tag[0].ToString();

        isConnectedImage.enabled = !isConnected;
    }
}