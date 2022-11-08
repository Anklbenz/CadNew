using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ControlParamsPreviewPanel : ControlParamsInputPanel {
    [SerializeField] private RectTransform photoPanelRect;
    private UniTaskCompletionSource _showCompletionSource;

    private CpPreview _cpPreview;

    protected override void Awake(){
        var itemFactory = new MenuItemFactory(menuItemInputPrefab, menuItemStatePrefab, menuItemDropDownPrefab, paramsItemsParent);
        _cpPreview = new CpPreview(itemFactory, paramsPanelTransform);
    } 

    public async UniTask PreviewProcess(Operation operation){
        Operation = operation;
        Initialize();
        
        _showCompletionSource = new UniTaskCompletionSource();
        await _showCompletionSource.Task;
    }

    protected override void Initialize(){
        _cpPreview.Initialize(Operation);
        
        if (Operation.Sprite != null){
            photoPanelRect.gameObject.SetActive(true);
            CpPhoto.SetImageOnPreviews(Operation.Sprite);
            
        }
        else{
            photoPanelRect.gameObject.SetActive(false);
        }
    }

    protected override void OnCloseClicked(){
        _showCompletionSource.TrySetResult();
    }
}
