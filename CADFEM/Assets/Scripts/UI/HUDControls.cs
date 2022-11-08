using System;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDControls : Window {
    [SerializeField] private Button settingsButton, exitButton;
    [SerializeField] private TMP_Text userInfoText, assetNameText;

    [SerializeField] private SettingsMenuControl settingsMenuControl;

    public void SetUserInfoAndAssetName(string userInfo, string assetName){
        userInfoText.text = userInfo;
        assetNameText.text = assetName;
    }

    private void OnSettingsClick(){
        settingsMenuControl.Open();
    }

    public void OnEnable() => settingsButton.onClick.AddListener(OnSettingsClick);


    private void OnDisable() => settingsButton.onClick.RemoveListener(OnSettingsClick);
}

