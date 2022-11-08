using System;
using System.Collections.Generic;
using ClassesForJsonDeserialize;
using UnityEngine;

public class SchemeDataIndicationController : MonoBehaviour {
    [Header(("LabelsSensors"))]
    [SerializeField] private LabelSensor assetNameLabel;

    [SerializeField] private LabelSensor batteryIdLabel;

    [Header(("States"))]
    [SerializeField] private ColoredLabelSensor batteryState;

    [SerializeField] private ColoredLabelSensor outputMode;

    [Header(("UL"))]
    [SerializeField] private TagValueSensor ul1;

    [SerializeField] private TagValueSensor ul2, ul3;

    [Header(("OutUL"))]
    [SerializeField] private TagValueSensor outUl1;

    [SerializeField] private TagValueSensor outUl2, outUl3;

    [Header(("OutIL"))]
    [SerializeField] private TagValueSensor outIl1;

    [SerializeField] private TagValueSensor outIl2, outIl3;

    [Header(("LoadL"))]
    [SerializeField] private TagValueSensor loadL1;

    [SerializeField] private TagValueSensor loadL2, loadL3;

    [Header(("Battery"))]
    [SerializeField] private TagValueSensor batteryUl1;

    [SerializeField] private TagValueSensor batteryTime, batteryTemp;

    [Header(("BypassUl"))]
    [SerializeField] private TagValueSensor bypassUl1;

    [SerializeField] private TagValueSensor bypassUl2, bypassUl3;

    [Header("Circles")]
    [SerializeField] private StateCircleSensor inverterCircle;

    [SerializeField] private StateCircleSensor rectifierCircle, switcherCircle;

    [Header("QSensors")]
    [SerializeField] private QSensor q1;

    [SerializeField] private QSensor q2, q4;

    public void Refresh(TagValues tagValues){
        var isConnected = tagValues.status.IsConnected;
        
        assetNameLabel.UpdateData(tagValues.base_labels.asset_name_tag);
        batteryIdLabel.UpdateData(tagValues.base_labels.battID_tag);

        outputMode.UpdateData(tagValues.status.OutputMode, tagValues.status.OutputMode_FillColor);
        batteryState.UpdateData(tagValues.status.BatteryState, tagValues.status.BatteryState_FillColor);

        q1.UpdateData(tagValues.Q1.value == 0); // датчик реверсивный
        q2.UpdateData(tagValues.Q2.value != 0);
        q4.UpdateData(tagValues.Q4.value != 0);

        ul1.UpdateData(tagValues.UL1, isConnected);
        ul2.UpdateData(tagValues.UL2, isConnected);
        ul3.UpdateData(tagValues.UL3, isConnected);
        outUl1.UpdateData(tagValues.outUL1, isConnected);
        outUl2.UpdateData(tagValues.outUL2, isConnected);
        outUl3.UpdateData(tagValues.outUL3, isConnected);
        outIl1.UpdateData(tagValues.outIL1, isConnected);
        outIl2.UpdateData(tagValues.outIL2, isConnected);
        outIl3.UpdateData(tagValues.outIL3, isConnected);
        loadL1.UpdateData(tagValues.loadL1, isConnected);
        loadL2.UpdateData(tagValues.loadL2, isConnected);
        loadL3.UpdateData(tagValues.loadL3, isConnected);
        batteryUl1.UpdateData(tagValues.batteryUL1, isConnected);
        batteryTime.UpdateData(tagValues.batteryTime, isConnected);
        batteryTemp.UpdateData(tagValues.batteryTemp, isConnected);
        bypassUl1.UpdateData(tagValues.bypassUL1, isConnected);
        bypassUl2.UpdateData(tagValues.bypassUL2, isConnected);
        bypassUl3.UpdateData(tagValues.bypassUL3, isConnected);
        inverterCircle.UpdateData(tagValues.state_circles.inverterCircle_state);
        rectifierCircle.UpdateData(tagValues.state_circles.rectifierCircle_state);
        switcherCircle.UpdateData(tagValues.state_circles.rectifierCircle_state);
    }
}
