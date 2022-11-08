using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColorPicker : MonoBehaviour {
    [SerializeField] private Image sampleColorImage;
    [SerializeField] private List<ColorPointer> colorsPointers;

    [SerializeField] private Slider opacitySlider;
    [SerializeField] private Text opacityLabel;
    [SerializeField] private Image opacityColorImage;

    [SerializeField] private Button closeButton;


    public Color CurrentColor => _currentColor;
    
    private Color _currentColor;
    private bool _clickOnPanel;

    public event Action<Color> OnColorSampledEvent;

    private void Awake(){
        opacitySlider.onValueChanged.AddListener(OpacitySampled);
        closeButton.onClick.AddListener(HidePanel);
        
        /*if (colorsPointers.Count > 0)
            ColorSampled(colorsPointers[0].Color);*/

        foreach (var colorPointer in colorsPointers)
            colorPointer.OnColorSampledEvent += ColorSampled;
    }

    private void OnDestroy(){
        opacitySlider.onValueChanged.RemoveListener(OpacitySampled);
        closeButton.onClick.RemoveListener(HidePanel);
        
        foreach (var colorPointer in colorsPointers)
            colorPointer.OnColorSampledEvent -= ColorSampled;
    }

    private void ColorSampled(Color color){
        _currentColor = color;

        sampleColorImage.color = color;
        opacityColorImage.color = color;

        OnColorSampledEvent?.Invoke(color);
    }

    private void OpacitySampled(float value){
        value = 1 - value;
        _currentColor.a = value;

        var opacityPercent = 100 * value;
        opacityLabel.text = $"{((int)opacityPercent).ToString()}%";

        OnColorSampledEvent?.Invoke(_currentColor);
    }

    private void HidePanel(){
        gameObject.SetActive(false);
    }
}
