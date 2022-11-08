using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using ClassesForJsonDeserialize;
using TMPro;

public class OperationsPanel : Window {
    private readonly Color OPERATION_BUTTON_COLOR_WHEN_IS_ACTIVE = new Color(0.95f, 0.73f, 0.15f);

    [Header("Prefabs")]
    [SerializeField] private OperationButton operationButtonPrefab;

    [Header("ContentContainers")]
    [SerializeField] private RectTransform operationButtonsParent;

    [SerializeField] private RectTransform referencesParent;
    [SerializeField] private RectTransform referencesHeaderParent;

    [Header("InformText")]
    [SerializeField] private TMP_Text operationNameText;

    [SerializeField] private TMP_Text operationDescriptionText;

    [Header("WindowControls")]
    [SerializeField] protected Button acceptButton, closeButton;

    [Header("Info Windows")]
    [SerializeField] private VideoPanel videoPanel;

    [SerializeField] private ImageInformationPanel imageInformationPanel;

    [SerializeField] private Button imageInfoButton, videoInfoButton, documentInfoButton;
    [SerializeField] private Toggle referenceCloseToggle, referenceHeaderCloseToggle;

    public List<OperationButton> OperationButtons{ get; } = new();

    protected Operation Operation;
    private UniTaskCompletionSource _operationCompletionSource;
    private OperationButton _operationButton;
   

// выделить Button list Class или класс Data где все лейблы кнопки и референсы
    public void SetImageDataGetter(RequestDataGetter requestDataGetter){
        imageInformationPanel.Initialize(requestDataGetter);
    }
    public virtual void SetOperation(Operation operation){
        Operation = operation;
        _operationButton = OperationButtons.First(button => button.Operation.id == Operation.id);
        RefreshData();
    }

    public async UniTask Process(){
        _operationCompletionSource = new UniTaskCompletionSource();
        await _operationCompletionSource.Task;
    }

    public void Initialize(Operation[] operations){
        if (OperationButtons.Count > 0)
            Clear();

        foreach (var operation in operations)
            AddOperationButton(operation);
    }

    private void Clear(){
        foreach (var button in OperationButtons)
            Destroy(button.gameObject);
        OperationButtons.Clear();
    }

    private void AddOperationButton(Operation operation){
        var item = Instantiate(operationButtonPrefab, operationButtonsParent);
        item.Initialize(operation);
        OperationButtons.Add(item);
    }

    private void RefreshData(){
        SetLabels();
        SetReferenceButtons();
        UpdateOperationButtonsColors();
        _operationButton.SetColor(OPERATION_BUTTON_COLOR_WHEN_IS_ACTIVE);
    }

    private void UpdateOperationButtonsColors(){
        foreach (var operationButton in OperationButtons)
            operationButton.UpdateColor();
    }

    private void SetLabels(){
        operationNameText.text = Operation.name;
        operationDescriptionText.text = Operation.description;
    }

    private void SetReferenceButtons(){
        documentInfoButton.interactable = Operation.url_document != null;
        imageInfoButton.interactable = Operation.url_image != null;
      //  videoInfoButton.interactable = Operation.url_video != null;
    }

    private void OnAcceptClick(){
        _operationCompletionSource.TrySetResult();
    }

    private void OnCloseClick(){
        _operationCompletionSource.TrySetCanceled();
    }

    private void OnReferenceHeaderToggleChange(bool state){
        referencesParent.gameObject.SetActive(state);
    }

    private void OnReferenceToggleChange(bool state){
        referencesParent.gameObject.SetActive(state);
        referencesHeaderParent.gameObject.SetActive(state);
    }

    private void OnVideoInfoClick(){
        videoPanel.Open();
        videoPanel.Initialize(Operation.url_video);
    }

    private void OnImageInfoClick(){
        imageInformationPanel.ShowImageAsync(Operation.url_image);
    }

    private void OnEnable(){
        acceptButton.onClick.AddListener(OnAcceptClick);
        closeButton.onClick.AddListener(OnCloseClick);
        videoInfoButton.onClick.AddListener(OnVideoInfoClick);
        imageInfoButton.onClick.AddListener(OnImageInfoClick);
        referenceCloseToggle.onValueChanged.AddListener(OnReferenceToggleChange);
        referenceHeaderCloseToggle.onValueChanged.AddListener(OnReferenceHeaderToggleChange);
    }
}