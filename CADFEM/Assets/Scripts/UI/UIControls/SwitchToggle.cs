using UnityEngine ;
using UnityEngine.UI ;

public class SwitchToggle : MonoBehaviour {
    [SerializeField] private RectTransform uiHandleRectTransform;
    [SerializeField] private Color backgroundActiveColor, handleActiveColor;

    private Toggle _toggle;
    private Vector2 _handlePosition;
    private Image _backgroundImage, _handleImage;
    private Color _backgroundDefaultColor, _handleDefaultColor;

    private void Awake(){
        _toggle = GetComponent<Toggle>();
        _handleImage = uiHandleRectTransform.GetComponent<Image>();
        _backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();

        _handlePosition = uiHandleRectTransform.anchoredPosition;
        _backgroundDefaultColor = _backgroundImage.color;
        _handleDefaultColor = _handleImage.color;
        _toggle.onValueChanged.AddListener(OnSwitch);

        OnSwitch(_toggle.isOn);
    }

    private void OnSwitch(bool on){
        uiHandleRectTransform.anchoredPosition = on ? _handlePosition  : _handlePosition * -1;
        _backgroundImage.color = on ? backgroundActiveColor : _backgroundDefaultColor;
        _handleImage.color = on ? handleActiveColor : _handleDefaultColor;
    }

    private void OnDestroy() => _toggle.onValueChanged.RemoveListener(OnSwitch);
}