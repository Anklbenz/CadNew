using ClassesForJsonDeserialize;
using UnityEngine;
using UnityEngine.UI;

public class QSensor : MonoBehaviour {
    [SerializeField] private Image onImage, offImage;

    public void UpdateData(bool state){
        onImage.gameObject.SetActive(state);
        offImage.gameObject.SetActive(!state);
    }
}
