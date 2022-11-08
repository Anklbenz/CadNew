using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPanel : Window {
    private const string VIDEO_URL = "https://svtaneko-migration.digitaltwin.ru:8443/Thingworx/FileRepositories/ServiceVizor_Media/video/v%20bypass%20SitePro.mp4";

    private const string PLAYBACK_ERROR_MESSAGE = "Ошибка воспроизведения. Возможные причины: \n - файл не найден; \n - файл не поддерживается; \n - отказанно в досптупе;";
    //http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4

    [SerializeField] private Toggle playToggle;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TimeBar timeBar;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private Button closeButton;

    [Header("Render")]
    [SerializeField] private RectTransform renderParent;

    [SerializeField] private RenderTexture rendererTexture;
    [SerializeField] private RawImage renderImage;

    private VideoPlayer _videoPlayer;
    private Fitter _fitter;
    
    private void Awake(){
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        _fitter = new Fitter();
    }
    
    private void FixedUpdate(){
        if (_videoPlayer.frameCount <= 0) return;
        progressBar.SetFillAmount(_videoPlayer.frame, _videoPlayer.frameCount);
        timeBar.Refresh((float)_videoPlayer.time, (float)_videoPlayer.clip.length);
    }

    public void Initialize(string url){
        errorText.text = string.Empty;
        playToggle.interactable = false;
        playToggle.isOn = false;

      //  _videoPlayer.url = "http://svtaneko-migration.digitaltwin.ru:8080/video/test.mp4";
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.targetTexture = rendererTexture;
        _videoPlayer.EnableAudioTrack(0, true);

        _videoPlayer.errorReceived += OnPlayerErrorReceived;
        
        _videoPlayer.Prepare();
        FitRenderImage();
    }

    private void FitRenderImage(){
        var clipSize = new Vector2(_videoPlayer.clip.width, _videoPlayer.clip.height);
        var parentSize = new Vector2(renderParent.rect.width, renderParent.rect.height);
        
        renderImage.rectTransform.sizeDelta = _fitter.FitRect(clipSize, parentSize);
    }

    private void PlayerIsReady(VideoPlayer player){
        playToggle.interactable = true;
        renderImage.enabled = true;
    }

    private void OnPlayToggleSwitched(bool state){
        if (state)
            _videoPlayer.Play();
        else
            _videoPlayer.Pause();
    }
    
    private void OnCloseClicked(){
        rendererTexture.Release();
        renderImage.enabled = false;
        Close();
    }
    
    private void Skip(float percent){
        var frame = _videoPlayer.frameCount * percent;
        _videoPlayer.frame = (long)frame;
    }
    
    private void OnPlayerErrorReceived(VideoPlayer source, string message){
        errorText.text = PLAYBACK_ERROR_MESSAGE;
        //_videoPlayer.
        _videoPlayer.errorReceived -= OnPlayerErrorReceived;
    }
    
    private void OnEnable(){
        progressBar.OnSkipEvent += Skip;
        _videoPlayer.prepareCompleted += PlayerIsReady;
       
        playToggle.onValueChanged.AddListener(OnPlayToggleSwitched);
        closeButton.onClick.AddListener(OnCloseClicked);
    }
    
    private void OnDisable(){
        progressBar.OnSkipEvent -= Skip;
        _videoPlayer.prepareCompleted -= PlayerIsReady;
        _videoPlayer.errorReceived -= OnPlayerErrorReceived;
        playToggle.onValueChanged.RemoveListener(OnPlayToggleSwitched);
        closeButton.onClick.RemoveListener(OnCloseClicked);
    }
}