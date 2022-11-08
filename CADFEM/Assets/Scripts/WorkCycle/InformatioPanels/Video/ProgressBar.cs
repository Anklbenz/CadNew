using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler {
    [SerializeField] private Image progressImage;
    public event Action<float> OnSkipEvent;

    public void OnDrag(PointerEventData eventData){
        TrySkip(eventData);
    }

    public void OnPointerDown(PointerEventData eventData){
        TrySkip(eventData);
    }

    private void TrySkip(PointerEventData eventData){
        if (!RectTransformUtility.RectangleContainsScreenPoint(progressImage.rectTransform, eventData.position)) return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(progressImage.rectTransform, eventData.position, null, out var localPoint)) return;
        var percent = Mathf.InverseLerp(progressImage.rectTransform.rect.xMin, progressImage.rectTransform.rect.xMax, localPoint.x);

        OnSkipEvent?.Invoke(percent);
    }

    public void SetFillAmount(float current, float total){
        progressImage.fillAmount = current / total;
    }
}
