using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ScreenshotWindow : Window {
    private readonly int _play = Animator.StringToHash("Play");
    private readonly int _stop = Animator.StringToHash("Stop");

    [SerializeField] private Image screenshotImage;
    [SerializeField] private TMP_Text hintLabel;
    [SerializeField] private Button shootButton, acceptButton, reshotButton, cancelButton, editButton;
    [SerializeField] private RectTransform takeShotPanel, acceptResultPanel;
    [SerializeField] private ScreenshotEditor screenshotEditor;
    [SerializeField] private Animator flashAnimator;

    private readonly Screenshot _screenshot = new();
    private UniTaskCompletionSource<Sprite> _screenShotCompletionSource;
    private Sprite _photoSprite;

    public async UniTask<Sprite> Process(){
        Initialize();
        _screenShotCompletionSource = new UniTaskCompletionSource<Sprite>();
        return await _screenShotCompletionSource.Task;
    }

    public void Initialize(){
        _photoSprite = null;
        screenshotImage.sprite = null;
        acceptResultPanel.gameObject.SetActive(false);
        takeShotPanel.gameObject.SetActive(true);
    }

    private void OnAcceptClick(){
        _screenShotCompletionSource.TrySetResult(screenshotImage.sprite);
    }

    private async void OnTakePhotoClick(){
        Hide();
        _photoSprite = await _screenshot.Take(this);
        Show();

        takeShotPanel.gameObject.SetActive(false);
        acceptResultPanel.gameObject.SetActive(true);
        flashAnimator.SetTrigger(_play);
        flashAnimator.SetTrigger(_stop);

        SetScreenshotImage(_photoSprite);
    }

    private void SetScreenshotImage(Sprite sprite){
        if (sprite != null)
            screenshotImage.sprite = sprite;
    }

    private void OnRepeatPhotoClick(){
        acceptResultPanel.gameObject.SetActive(false);
        takeShotPanel.gameObject.SetActive(true);
    }

    private void OnCancelClick(){
        _screenShotCompletionSource.TrySetCanceled();
    }

    private async void OnEditClick(){
        takeShotPanel.gameObject.SetActive(false);
        screenshotEditor.Prepare(_photoSprite);
        screenshotEditor.Open();
        var editedSprite = await screenshotEditor.GetEditedImage();
        screenshotEditor.Close();
        takeShotPanel.gameObject.SetActive(true);
        SetScreenshotImage(editedSprite);
    }

    private void OnEnable(){
        acceptButton.onClick.AddListener(OnAcceptClick);
        shootButton.onClick.AddListener(OnTakePhotoClick);
        editButton.onClick.AddListener(OnEditClick);
        reshotButton.onClick.AddListener(OnRepeatPhotoClick);
        cancelButton.onClick.AddListener(OnCancelClick);
    }

    private void OnDisable(){
        acceptButton.onClick.RemoveListener(OnAcceptClick);
        shootButton.onClick.RemoveListener(OnTakePhotoClick);
        reshotButton.onClick.RemoveListener(OnRepeatPhotoClick);
        cancelButton.onClick.RemoveListener(OnCancelClick);
    }
}