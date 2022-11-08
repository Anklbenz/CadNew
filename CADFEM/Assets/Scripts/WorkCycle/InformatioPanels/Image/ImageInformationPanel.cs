using UnityEngine;
using UnityEngine.UI;

public class ImageInformationPanel : Window {
        [SerializeField] private Button closeButton;
        [SerializeField] private RawImage downloadedImage;
        [SerializeField] private RectTransform contentParent, windowRectTransform;

        private RequestDataGetter _requestDataGetter;
        private Fitter _fitter;

        public void Initialize(RequestDataGetter requestDataGetter){
                _requestDataGetter = requestDataGetter;
                _fitter = new Fitter();
        }

        public async void ShowImageAsync(string url){
                var texture = await _requestDataGetter.GetTexture(url);
                if (texture == null) return;

                var textureSize = new Vector2(texture.width, texture.height);
                var parentSize = new Vector2(contentParent.rect.width, contentParent.rect.height);
                downloadedImage.rectTransform.sizeDelta = _fitter.FitRect(textureSize, parentSize);
                downloadedImage.texture = texture;
                Open();
        }

        private void OnEnable(){
                closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnDisable(){
                closeButton.onClick.RemoveListener(OnCloseClick);
        }

        private void OnCloseClick(){
                Close();
        }
}