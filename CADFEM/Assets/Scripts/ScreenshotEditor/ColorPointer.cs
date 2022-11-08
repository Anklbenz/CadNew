using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPointer : MonoBehaviour, IPointerDownHandler {
    [SerializeField] private Image sampleImage;
    
    public Color color;

    [Range(0, 1)]
    [SerializeField] private float colorAlpha;
    
    public event Action<Color> OnColorSampledEvent;

    private void OnValidate(){
        sampleImage.color = color;
    }

    public void OnPointerDown(PointerEventData eventData){
        color.a = colorAlpha;
        OnColorSampledEvent?.Invoke(color);
    }
}
