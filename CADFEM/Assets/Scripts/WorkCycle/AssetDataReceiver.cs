using System;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;

public class AssetDataReceiver {
    public event Action OnTagDataReceivedEvent;
    public event Action<bool> OnTasksAvailableEvent;
    public TagValues TagValues{ get; private set; }
    public TaskData[] Tasks{ get; private set; }

    private readonly ThingWorksServices _thingWorksServices;
    private readonly string _assetName;

    public AssetDataReceiver(ThingWorksServices thingWorksServices){
        _thingWorksServices = thingWorksServices;
    }

    public async UniTask GetSensorsData(string assetName){
        TagValues = await _thingWorksServices.GetAssetTagValues(assetName);

        if (TagValues != null) 
            OnTagDataReceivedEvent?.Invoke();
    }

    public async UniTask GetAvailableTasks(string assetName){
        Tasks  = await _thingWorksServices.RequestAndWorkLogGetView(assetName);

        OnTasksAvailableEvent?.Invoke(Tasks.Length > 0);
    }
}
