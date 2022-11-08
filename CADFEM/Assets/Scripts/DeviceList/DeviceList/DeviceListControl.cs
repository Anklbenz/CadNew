using System;
using UnityEngine;

public class DeviceListControl : MonoBehaviour {
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private DeviceListItem itemPrefab;
    
    public event Action<Device> ItemSelectedEvent;

    public void Add(Device device){
        var item = Instantiate(itemPrefab, contentParent);
        item.Initialize(device, ItemSelectedEvent);
    }
}
