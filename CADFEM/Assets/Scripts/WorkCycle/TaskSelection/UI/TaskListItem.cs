using System;
using ClassesForJsonDeserialize;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskListItem : MonoBehaviour {
    private readonly Color FONT_COLOR_WHEN_ITEM_IS_ON = Color.white;
    private readonly Color FONT_COLOR_WHEN_ITEM_IS_OFF = new Color(0.3113f, 0.3113f, 0.3113f);

    [SerializeField] private TMP_Text typeText, operationIdText, statusText, commentText;
    [SerializeField] private Image highlightedTextBackground;
    [SerializeField] private Toggle selectToggle;

    public event Action<TaskData> OnTaskSelectEvent;
    private TaskData _taskData;

    public void SetData(TaskData taskData, ToggleGroup listToggleGroup){
        _taskData = taskData;

        typeText.text = taskData.row_type_name;
        operationIdText.text = taskData.row_code;
        statusText.text = taskData.row_status_name;
        commentText.text = taskData.row_comment;

        if (ColorUtility.TryParseHtmlString(taskData.row_status_color, out var color))
            highlightedTextBackground.color = color;

        selectToggle.group = listToggleGroup;
        selectToggle.onValueChanged.AddListener(OnValueChanged);
        selectToggle.onValueChanged.AddListener(OnValueChangedChangeTextColor);
    }

    private void OnValueChanged(bool toggleValue){
        OnTaskSelectEvent?.Invoke(toggleValue ? _taskData : new TaskData());
    }

    private void OnValueChangedChangeTextColor(bool toggleValue){
        typeText.color = operationIdText.color = commentText.color = toggleValue ? FONT_COLOR_WHEN_ITEM_IS_ON : FONT_COLOR_WHEN_ITEM_IS_OFF;
    }

    private void OnDestroy() => selectToggle.onValueChanged.RemoveAllListeners();
}