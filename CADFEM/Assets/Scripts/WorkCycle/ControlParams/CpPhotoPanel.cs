using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CpPhotoPanel : IDisposable {
   private const float FULL_PREVIEW_IMAGE_INDENT = 0.9f;

   public event Action GettingPhotoBeginEvent;
   public event Action GettingPhotoFinishedEvent;
   public Sprite PhotoSprite{ get; private set; }

   private readonly ScreenshotWindow _screenshotWindow;
   private readonly Image _iconPreviewImage, _fullPreviewImage;
   private readonly Button _takePhotoButton, _deletePhotoButton, _fullPreviewButton, _iconPreviewButton;

   public CpPhotoPanel(ScreenshotWindow screenshotWindow, Button takePhotoButton, Button deletePhotoButton, Button iconPreviewButton, Button fullPreviewButton, Image iconPreview, Image fullPreview){
      _screenshotWindow = screenshotWindow;
      _takePhotoButton = takePhotoButton;
      _deletePhotoButton = deletePhotoButton;
      _fullPreviewButton = fullPreviewButton;
      _iconPreviewButton = iconPreviewButton;

      _iconPreviewImage = iconPreview;
      _fullPreviewImage = fullPreview;

      _takePhotoButton.onClick.AddListener(OnTakePhotoClicked);
      _deletePhotoButton.onClick.AddListener(OnDeletePhotoClicked);
      _iconPreviewButton.onClick.AddListener(OnIconPreviewButtonClicked);
      _fullPreviewButton.onClick.AddListener(OnFullPreviewButtonClicked);

      SetFullPreviewImageSize();
   }

   private async void OnTakePhotoClicked(){
      GettingPhotoBeginEvent?.Invoke();
      _screenshotWindow.Open();
      _screenshotWindow.Initialize();
      UiVisibleController.Instance.HideAll();

      PhotoSprite = await TryGetPhoto();

      UiVisibleController.Instance.ShowAll();
      _screenshotWindow.Close();
      GettingPhotoFinishedEvent?.Invoke();

      SetImageOnPreviews(PhotoSprite);
      SetPanelView();
   }

   private async UniTask<Sprite> TryGetPhoto(){
      try{
         return await _screenshotWindow.Process();
      }
      catch (OperationCanceledException oe){
         return null;
      }
   }

   private void OnDeletePhotoClicked(){
      SetImageOnPreviews(null);
      Clear();
   }

   private void OnIconPreviewButtonClicked(){
      _fullPreviewImage.gameObject.SetActive(true);
   }

   private void OnFullPreviewButtonClicked(){
      _fullPreviewImage.gameObject.SetActive(false);
   }

   public void SetPanelView(){
      PhotoPanelSetMode(PhotoSprite ? PanelMode.PhotoReady : PanelMode.PhotoNotReady);
   }

   public void Clear(){
      PhotoSprite = null;
      _iconPreviewImage.sprite = null;
      _fullPreviewImage.sprite = null;

      PhotoPanelSetMode(PanelMode.PhotoNotReady);
   }

   public void SetImageOnPreviews(Sprite sprite){
      _iconPreviewImage.sprite = sprite;
      _fullPreviewImage.sprite = sprite;
   }


   private void PhotoPanelSetMode(PanelMode panelMode){
      switch (panelMode){
         case PanelMode.PhotoReady:
            _iconPreviewButton.interactable = true;
            _iconPreviewImage.gameObject.SetActive(true);
            _deletePhotoButton.gameObject.SetActive(true);
            break;

         case PanelMode.PhotoNotReady:
            _iconPreviewImage.gameObject.SetActive(false);
            _fullPreviewImage.gameObject.SetActive(false);
            _iconPreviewButton.interactable = false;
            _deletePhotoButton.gameObject.SetActive(false);
            break;
      }
   }

   private void SetFullPreviewImageSize(){
      var preferredSize = new Vector2(Screen.width, Screen.height);
      preferredSize *= FULL_PREVIEW_IMAGE_INDENT;
      _fullPreviewImage.rectTransform.sizeDelta = preferredSize;
   }

   private enum PanelMode {
      PhotoReady = 0,
      PhotoNotReady = 1,
   }

   public void Dispose(){
      _takePhotoButton.onClick.RemoveListener(OnTakePhotoClicked);
      _deletePhotoButton.onClick.RemoveListener(OnDeletePhotoClicked);
      _iconPreviewButton.onClick.RemoveListener(OnIconPreviewButtonClicked);
      _fullPreviewButton.onClick.RemoveListener(OnFullPreviewButtonClicked);
   }
}