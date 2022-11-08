using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DeviceLoader : MonoBehaviour {
    [SerializeField] private List<Device> deviceList;
    [SerializeField] private DeviceListControl deviceListControl;
    [SerializeField] private TransferObject transferObject;
    
    private void Start(){
        deviceListControl.ItemSelectedEvent += PrepareAndCreate;

        foreach (var device in deviceList)
            deviceListControl.Add(device);
    }

    private void OnDestroy(){
        deviceListControl.ItemSelectedEvent -= PrepareAndCreate;
    }

    private void PrepareAndCreate(Device device){
        transferObject.prefab = device;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
