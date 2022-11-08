using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuControl : Window {
    [SerializeField] private Button closeButton;

    [Header("Menu Toggles")]
    [SerializeField] private Toggle modelViewToggle;

    [SerializeField] private Toggle onFillingViewToggle, forceLineToggle, schemeViewToggle, rotateSensorToggle;

    public event Action<bool> OnModelVisibleTurnEvent, 
        OnFilingViewTurnEvent,
        OnHighlightTurnEvent,
        OnSchemeViewTurnEvent,
        OnLookAtSwitchedEvent;

    private void Awake(){
        closeButton.onClick.AddListener(Close);
        modelViewToggle.onValueChanged.AddListener(ModelViewMode);
        onFillingViewToggle.onValueChanged.AddListener(FilingViewMode);
        forceLineToggle.onValueChanged.AddListener(HighlightViewMode);
        schemeViewToggle.onValueChanged.AddListener(SchemeViewMode);
        rotateSensorToggle.onValueChanged.AddListener(SensorTurnMode);
    }

    private void ModelViewMode(bool state) => OnModelVisibleTurnEvent?.Invoke(state);
    private void FilingViewMode(bool state) => OnFilingViewTurnEvent?.Invoke(state);
    private void HighlightViewMode(bool state) => OnHighlightTurnEvent?.Invoke(state);
    private void SchemeViewMode(bool state) => OnSchemeViewTurnEvent?.Invoke(state);
    private void SensorTurnMode(bool state) => OnLookAtSwitchedEvent?.Invoke(state);
}
