using System;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class DrawingUIController : MonoBehaviour {
    [Header("Buttons")]
    [SerializeField] private Button pickColorButton;

    [SerializeField] private Button lineDrawButton, arrowDrawButton, noteButton, sizeSetButton, undoButton;

    [Header("SelectedColorImage")]
    [SerializeField] private Image colorImage;

    public event Action OnLineTapEvent, OnArrowTapEvent, OnUndoTapEvent;

    public ColorPicker colorPicker;
    public SizeSetter sizeSetter;
    public GameObject notePanel;

    private void Awake(){
        sizeSetButton.onClick.AddListener(SizeSetterActivate);
        pickColorButton.onClick.AddListener(ColorPickerSetActivate);

        lineDrawButton.onClick.AddListener(LineSelected);
        arrowDrawButton.onClick.AddListener(ArrowSelected);
        noteButton.onClick.AddListener(NoteEditorSetActivate);
        undoButton.onClick.AddListener(UndoTap);

        colorPicker.OnColorSampledEvent += SetPikerColor;
    }

    private void SetPikerColor(Color color)=>  colorImage.color = color;
    private void ColorPickerSetActivate() => colorPicker.gameObject.SetActive(true);
    private void NoteEditorSetActivate() => notePanel.gameObject.SetActive(true);
    private void SizeSetterActivate() => sizeSetter.gameObject.SetActive(true);

    private void LineSelected(){
        //... here highlight appear
        OnLineTapEvent?.Invoke();
    }

    private void ArrowSelected(){
        // ... here highlight appear
        OnArrowTapEvent?.Invoke();
    }

    private void UndoTap(){
        OnUndoTapEvent?.Invoke();
    }
}