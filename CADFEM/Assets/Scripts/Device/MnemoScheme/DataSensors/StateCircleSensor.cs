using UnityEngine;
using UnityEngine.UI;

public class StateCircleSensor : MonoBehaviour {
    [SerializeField] private Image stateImage;

    public void UpdateData(string colorString){
        if (ColorUtility.TryParseHtmlString(colorString, out var color))
            stateImage.color = color;
    }
}
