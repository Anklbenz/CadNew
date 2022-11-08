using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ClassesForJsonDeserialize;
using RequestParamClasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TasksSelectPanel : Window {
    private const string REQUEST_CODE = "REQUEST";
    private const string WORK_LOG_CODE = "WORK_LOG";
    private const string CREATE_WORK_LOG_LABEL = "Создать обход";
    private const string PERFORM_WORK_LOG_LABEL = "Выполнить обход";
    private const string SELECT_TASK_LABEL = "Сделайте выбор";
    
    [SerializeField] private TMP_Text userInfoText;
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private TaskListItem itemPrefab;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Button confirmButton, closeButton;
    [SerializeField] private TMP_Text confirmButtonText;

    private readonly List<TaskListItem> _taskListItems = new();
    private UniTaskCompletionSource<TaskData> _selectTaskCompletionSource;
    private TaskData _selectedTask;

    public async UniTask<TaskData> TaskSelectionProcess(){
        _selectTaskCompletionSource = new UniTaskCompletionSource<TaskData>();
        return await _selectTaskCompletionSource.Task;
    }

    public void SetUserInfo(string userInfo){
        userInfoText.text = userInfo;
    }

    public void Initialize(TaskData[] taskList){
        Clear();
        foreach (var task in taskList)
            AddListItem(task);
    }

    private void OnConfirmClicked(){
        if (_selectedTask == null) return;

        if (!_selectTaskCompletionSource.TrySetResult(_selectedTask))
            Debug.Log("Task select not success");
    }

    private void OnCloseClicked(){
        _selectTaskCompletionSource.TrySetCanceled();
    }

    private void AddListItem(TaskData task){
        var item = Instantiate(itemPrefab, contentParent);
        item.SetData(task, toggleGroup);
        item.OnTaskSelectEvent += SelectTask;
        item.OnTaskSelectEvent += SetConfirmButtonInteractable;
        item.OnTaskSelectEvent += SetConfirmButtonLabel;

        _taskListItems.Add(item);
    }

    private void Clear(){
        foreach (var item in _taskListItems){
            item.OnTaskSelectEvent -= SelectTask;
            item.OnTaskSelectEvent -= SetConfirmButtonInteractable;
            item.OnTaskSelectEvent -= SetConfirmButtonLabel;
            Destroy(item.gameObject);
        }

        _taskListItems.Clear();
    }

    private void SelectTask(TaskData task) => _selectedTask = task;
    private void SetConfirmButtonInteractable(TaskData task) => confirmButton.interactable = task.row_type_code != null;

    private void SetConfirmButtonLabel(TaskData task){
        confirmButtonText.text = task.row_type_code switch
        {
            (REQUEST_CODE) => CREATE_WORK_LOG_LABEL,
            (WORK_LOG_CODE) => PERFORM_WORK_LOG_LABEL,
            _ => SELECT_TASK_LABEL
        };
    }

    private void OnEnable(){
        confirmButton.onClick.AddListener(OnConfirmClicked);
        closeButton.onClick.AddListener(OnCloseClicked);
    }

    private void OnDisable(){
        confirmButton.onClick.RemoveListener(OnCloseClicked);
        closeButton.onClick.RemoveListener(OnConfirmClicked);
    }
}