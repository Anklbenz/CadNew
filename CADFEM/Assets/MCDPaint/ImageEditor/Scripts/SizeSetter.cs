using System;
using UnityEngine;
using UnityEngine.UI;

public class SizeSetter : MonoBehaviour {
    private const float MAX_SIZE_VALUE = 0.5f;
    private const float MIN_SIZE_VALUE = 0.1f;
    
    [Header(("Header"))]
    [SerializeField] private Image circleImage;

    [SerializeField] private Slider multiSizeSlider;
    [SerializeField] private Button closeButton;

    [Range(MIN_SIZE_VALUE, MAX_SIZE_VALUE)]
    [SerializeField] private float sliderMinValue, sliderMaxValue;

    public event Action<float> OnBrushSizeChangedEvent;

    private float _ratio;
    private Vector2 _startCircleImageSize;

    private void Awake(){
        multiSizeSlider.minValue = sliderMinValue;
        multiSizeSlider.maxValue = sliderMaxValue;
        _ratio = sliderMaxValue / sliderMinValue;

        _startCircleImageSize = circleImage.rectTransform.sizeDelta;

        multiSizeSlider.onValueChanged.AddListener(BrushSizeChange);
        multiSizeSlider.onValueChanged.AddListener(UICircleImageSizeChange);
        closeButton.onClick.AddListener(PanelClose);
    }

    private void BrushSizeChange(float multiplier){
        OnBrushSizeChangedEvent?.Invoke(multiplier);
    }

    private void UICircleImageSizeChange(float multiplier){
        circleImage.rectTransform.sizeDelta = _startCircleImageSize * _ratio * multiplier / sliderMaxValue;
    }

    private void PanelClose(){
        gameObject.SetActive(false);
    }
}
