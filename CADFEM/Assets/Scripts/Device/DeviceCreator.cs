using UnityEngine;

public class DeviceCreator {
    public Device Create(Device prefab, Transform deviceParent){
        return Object.Instantiate(prefab, deviceParent.position, Quaternion.identity, deviceParent.transform);
    }
}
