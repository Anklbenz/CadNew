using System;
using RequestParamClasses;
using ClassesForJsonDeserialize;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class ControlParamsMenuItem : MonoBehaviour {
    protected const string NOMINAL_VALUE_TEXT = "Стандартное знач.";
    private readonly Color VALID_DATA_BACK_COLOR = new(0.862f, 1f, 0.868f);
    private readonly Color INVALID_DATA_BACK_COLOR = new(0.980f, 0.663f, 0.639f);

    [SerializeField] protected TMP_Text descriptionText;
    [SerializeField] private Image inputBackgroundImage;

    public Action DataChangedEvent;
    public abstract bool IsDataFilled{ get; }
    public abstract bool ReadOnly{ get; set; }
    public ControlParamForSave GetData => Data;
    protected ControlParamForSave Data;

    public virtual void Initialize(ControlParam controlParam){
        Data = new ControlParamForSave { work_log_operation_cp_id = controlParam.id };
        descriptionText.text = controlParam.name;
        HighlightDataBackground();
    }

    protected void HighlightDataBackground(){
        inputBackgroundImage.color = IsDataFilled ? VALID_DATA_BACK_COLOR : INVALID_DATA_BACK_COLOR;
    }

    public abstract void SetData(ControlParam data);

    public void Delete(){
        Destroy(gameObject);
    }
}
