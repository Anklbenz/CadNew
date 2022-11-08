using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CommentPopup : Popup {
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text placeholderText;

    private UniTaskCompletionSource<string> _dialogResultCompletionSource;

    public static CommentPopup Instance{ get; protected set; }
    public  void Initialize(){
        Instance = this;
    }
    
    public async UniTask<string> AwaitForDecisionWithComment(string msg, string placeHolderHint, string windowLabel = CONFIRM_HEADER){
        Open();
        _dialogResultCompletionSource = new UniTaskCompletionSource<string>();
        headerText.text = windowLabel;
        placeholderText.text = placeHolderHint;
        contentText.text = msg;
        var result = await _dialogResultCompletionSource.Task;

        Close();
        return result;
    }
    
    protected override void SetAcceptResultOnClick() => _dialogResultCompletionSource.TrySetResult(inputField.text);

    private void OnEnable(){
        acceptButton.onClick.AddListener(SetAcceptResultOnClick);
    }

    private void OnDisable(){
        acceptButton.onClick.RemoveListener(SetAcceptResultOnClick);
    }
}
