using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopup : Popup {
    private const string ALERT_HEADER = "Требования к безопастности";
 
    private UniTaskCompletionSource _dialogResultCompletionSource;
   
    public static WarningPopup Instance{ get; protected set; }
    public  void Initialize(){
        Instance = this;
    }
    
    public async UniTask AwaitForAccept(string msg, string windowLabel = ALERT_HEADER){
        Open();

        _dialogResultCompletionSource = new UniTaskCompletionSource();
        headerText.text = windowLabel;
        contentText.text = msg;
       
        await _dialogResultCompletionSource.Task;

        Close();
    }

    protected override void SetAcceptResultOnClick() => _dialogResultCompletionSource.TrySetResult();
    
    private void OnEnable(){
        acceptButton.onClick.AddListener(SetAcceptResultOnClick);
    }

    private void OnDisable(){
        acceptButton.onClick.RemoveListener(SetAcceptResultOnClick);
    }
}
