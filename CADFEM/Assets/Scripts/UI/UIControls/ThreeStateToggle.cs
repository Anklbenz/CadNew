
using UnityEngine;
using UnityEngine.UI;

public class ThreeStateToggle : MonoBehaviour {
    private readonly Color GREEN = new Color(0.6f, 0.88f, 0.45f);
    private readonly Color RED = new Color(0.88f, 0.12f, 0.06f);

    [SerializeField] private Toggle toggleInput;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Image switchImage;

    private void SetSwitchColor(bool state) => switchImage.color = state ? GREEN : RED;

   // private void OnValidate() => backgroundImage.color = backgroundColor;

    private void OnEnable() => toggleInput.onValueChanged.AddListener(SetSwitchColor);

    private void OnDisable() => toggleInput.onValueChanged.RemoveListener(SetSwitchColor);

}
