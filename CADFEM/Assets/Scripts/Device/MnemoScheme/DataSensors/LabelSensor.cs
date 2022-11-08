using TMPro;
using UnityEngine;

public class LabelSensor : MonoBehaviour {
    [SerializeField] protected TMP_Text baseLabel;

    public void UpdateData(string label){
        baseLabel.text = label;
    }
}
