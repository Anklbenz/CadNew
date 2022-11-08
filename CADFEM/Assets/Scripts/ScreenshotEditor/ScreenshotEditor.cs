using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenshotEditor : Window, IDragHandler, IDropHandler {
    private const float BRUSH_SIZE = 25f;
    [SerializeField] private Image drawingRectImage;
    [SerializeField] private LineFactory lineFactory;
    [SerializeField] private Drawer drawer;
    [Header("Controls Menu")]
    [SerializeField] private Toggle controlsHideToggle;
    [SerializeField] private RectTransform controlsMenuTransform;
    [SerializeField] private Toggle lineToggle, arrowToggle;
    [SerializeField] private Button acceptButton, cancelButton, undoButton;
    [SerializeField] private ColorPointer[] colorPointers;

    private UniTaskCompletionSource<Sprite> _taskCompletionSource;
    private readonly Stack<Line> _undoStack = new Stack<Line>();
    private readonly Screenshot _screenshot = new();
    private Sprite Sprite{ get; set; }

    private BrushType _brushType;
    private float _width;
    private Color _color;

    public void Prepare(Sprite screenshot){
        Sprite = screenshot;
        drawingRectImage.sprite = screenshot;
        controlsMenuTransform.gameObject.SetActive(true);
        
        Clear();
    }
    
    public async UniTask<Sprite> GetEditedImage(){
        _taskCompletionSource = new UniTaskCompletionSource<Sprite>();
        return await _taskCompletionSource.Task;
    }
    
    private void DoUndo(){
        if (_undoStack.Count == 0) return;

        var line = _undoStack.Pop();
        Destroy(line.gameObject);
    }

    private async void OnAcceptClick(){
        Hide();
        Sprite = await _screenshot.Take(this);
        Show();

        _taskCompletionSource.TrySetResult(Sprite);
    }

    private void OnCloseClick(){
        Close();
    }

    public void OnDrag(PointerEventData eventData){
        if (drawer.Line == null){
            drawer.Line = lineFactory.GetBrush(_brushType);
            drawer.Line.Initialize(_color, BRUSH_SIZE);
        }

        drawer.Draw(eventData.position);
    }

    public void OnDrop(PointerEventData eventData){
        if (drawer.Line.PositionsCount > 1)
            _undoStack.Push(drawer.Line);

        drawer.StopDraw();
    }

    private void SetLineTool(bool state){
        if (state) _brushType = BrushType.Line;
    }

    private void SetArrowTool(bool state){
        if (state) _brushType = BrushType.Arrow;
    }

    private void SetColor(Color color) => _color = color;

    private void Clear(){
        foreach (var line in _undoStack)
            Destroy(line.gameObject);

        _undoStack.Clear();
    }
    
    private void ControlsMenuEnabled(bool state){
        controlsMenuTransform.gameObject.SetActive(state);
    }

    private void OnEnable(){
        foreach (var colorPicker in colorPointers)
            colorPicker.OnColorSampledEvent += SetColor;
        if (colorPointers.Length > 0) SetColor(colorPointers[0].color);

        acceptButton.onClick.AddListener(OnAcceptClick);
        cancelButton.onClick.AddListener(OnCloseClick);
        undoButton.onClick.AddListener(DoUndo);
        lineToggle.onValueChanged.AddListener(SetLineTool);
        arrowToggle.onValueChanged.AddListener(SetArrowTool);

        controlsHideToggle.onValueChanged.AddListener(ControlsMenuEnabled);
    }

    private void OnDisable(){
        foreach (var colorPicker in colorPointers)
            colorPicker.OnColorSampledEvent -= SetColor;
        
        acceptButton.onClick.RemoveListener(OnAcceptClick);
        cancelButton.onClick.RemoveListener(OnCloseClick);
        undoButton.onClick.RemoveListener(DoUndo);
        lineToggle.onValueChanged.RemoveListener(SetLineTool);
        arrowToggle.onValueChanged.RemoveListener(SetArrowTool);
        
        controlsHideToggle.onValueChanged.RemoveListener(ControlsMenuEnabled);
    }
}
