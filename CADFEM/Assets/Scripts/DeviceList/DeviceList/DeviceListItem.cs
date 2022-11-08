using System;
using UnityEngine;
using UnityEngine.UI;

public class DeviceListItem : MonoBehaviour{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemLabel, itemDescription;
    [SerializeField] private Button button;
    
    public  void Initialize (Device device, Action<Device> callback){
        /*itemImage.sprite = device.DDescription.PresentImage;
        itemLabel.text = device.Label;
        itemDescription.text = device.Description;*/

        button.onClick.AddListener(delegate { callback(device); });
    }

    private void OnDestroy() => button.onClick.RemoveAllListeners();
}