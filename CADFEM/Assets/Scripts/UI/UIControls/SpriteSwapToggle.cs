using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapToggle : MonoBehaviour {
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image isOnImage, isOffImage;

    private void SpriteSwap(bool state){
        isOnImage.enabled = state ;
        isOffImage.enabled = !state;
    }

    private void OnEnable(){
        SpriteSwap(toggle.isOn);
        toggle.onValueChanged.AddListener(SpriteSwap);
    }

    private void OnDisable()=> toggle.onValueChanged.RemoveListener(SpriteSwap);
}
