using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AlertPopup : Popup {
    [SerializeField] protected Button cancelButton;
    
    private UniTaskCompletionSource<bool> _dialogResultCompletionSource;
    
    public static AlertPopup Instance{ get; protected set; }
    public  void Initialize(){
        Instance = this;
    }

    public async UniTask<bool> AwaitForDecision(string msg, string windowLabel = CONFIRM_HEADER){
        Open();
        
        _dialogResultCompletionSource = new UniTaskCompletionSource<bool>();
        cancelButton.gameObject.SetActive(true);

        headerText.text = windowLabel;
        contentText.text = msg;
        var result = await _dialogResultCompletionSource.Task;

        Close();
        return result;
    }
    
    protected override void SetAcceptResultOnClick() => _dialogResultCompletionSource.TrySetResult(true);
    private void SetCancelResultOnClick() => _dialogResultCompletionSource.TrySetResult(false);
    
    private void OnEnable(){
        acceptButton.onClick.AddListener(SetAcceptResultOnClick);
        cancelButton.onClick.AddListener(SetCancelResultOnClick);
    }

    private void OnDisable(){
        acceptButton.onClick.RemoveListener(SetAcceptResultOnClick);
        cancelButton.onClick.RemoveListener(SetCancelResultOnClick);
    }
}
