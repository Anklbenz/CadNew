using System;
using Enums;
using System.Collections.Generic;
using System.Linq;
using ClassesForJsonDeserialize;
using UnityEngine;

[System.Serializable]
public class HighlightStates {
// состояния названы по приципу: если выключатель включен пишем его имя в название переменной, если нет не пишем ничего 
    [SerializeField]
    private Transform[] inverterQ1Q2Q4,
        inverter,
        inverterQ1,
        inverterQ1Rectifier,
        inverterQ1Q4,
        inverterQ1Q2,
        inverterQ2,
        inverterQ2Q4,
        q1Q2Q4,
        allOff,
        q1,
        q1Rectifier,
        q1Q4,
        q1Q2,
        q2,
        q2Q4,
        q4;

    public List<Transform> AllHighlightsElements{ get; set; } = new();

    public void Initialize(){
        inverterQ1Q2Q4 = GetAllChildes(inverterQ1Q2Q4);
        inverter = GetAllChildes(inverter);
        inverterQ1 = GetAllChildes(inverterQ1);
        inverterQ1Rectifier = GetAllChildes(inverterQ1Rectifier);
        inverterQ1Q4 = GetAllChildes(inverterQ1Q4);
        inverterQ1Q2 = GetAllChildes(inverterQ1Q2);
        inverterQ2 = GetAllChildes(inverterQ2);
        inverterQ2Q4 = GetAllChildes(inverterQ2Q4);
        q1Q2Q4 = GetAllChildes(q1Q2Q4);
        allOff = GetAllChildes(allOff);
        q1 = GetAllChildes(q1);
        q1Rectifier = GetAllChildes(q1Rectifier);
        q1Q4 = GetAllChildes(q1Q4);
        q1Q2 = GetAllChildes(q1Q2);
        q2 = GetAllChildes(q2);
        q2Q4 = GetAllChildes(q2Q4);
        q4 = GetAllChildes(q4);

        AllUniqueHighlightsInit();
    }

    private void AllUniqueHighlightsInit(){
        AllHighlightsElements.AddRange(inverterQ1Q2Q4);
        AllHighlightsElements.AddRange(inverter);
        AllHighlightsElements.AddRange(inverterQ1Q2Q4);
        AllHighlightsElements.AddRange(inverter);
        AllHighlightsElements.AddRange(inverterQ1);
        AllHighlightsElements.AddRange(inverterQ1Rectifier);
        AllHighlightsElements.AddRange(inverterQ1Q4);
        AllHighlightsElements.AddRange(inverterQ1Q2);
        AllHighlightsElements.AddRange(inverterQ2);
        AllHighlightsElements.AddRange(inverterQ2Q4);
        AllHighlightsElements.AddRange(q1Q2Q4);
        AllHighlightsElements.AddRange(allOff);
        AllHighlightsElements.AddRange(q1);
        AllHighlightsElements.AddRange(q1Rectifier);
        AllHighlightsElements.AddRange(q1Q4);
        AllHighlightsElements.AddRange(q1Q2);
        AllHighlightsElements.AddRange(q2);
        AllHighlightsElements.AddRange(q2Q4);
        AllHighlightsElements.AddRange(q4);

        AllHighlightsElements = new HashSet<Transform>(AllHighlightsElements).ToList();
    }

    public HighlightState GetState(TagValues tagValues){
        var Q1 = tagValues.Q1.value == 0; // Q1 тег показывает наоборот при true 0, при false 1
        var Q2 = tagValues.Q2.value != 0;
        var Q4 = tagValues.Q4.value != 0;

        var Inverter = tagValues.state_circles.inverterCircle_value != 0;
        //var rectifier = q4//tagValues.state_circles.rectifierCircle_value != 0;

        if (Inverter){
            if (Q1 && Q2 && Q4) return HighlightState.InverterQ1Q2Q4;
            if (!Q1 && !Q2 && !Q4) return HighlightState.Inverter;
            if (Q1 && !Q2 && !Q4) return HighlightState.InverterQ1;
            //  if (q1 && !q2 && !q4 && rectifier) return HighlightState.InverterQ1Rectifier;
            if (Q1 && Q2 && !Q4) return HighlightState.InverterQ1Q2;
            if (Q1 && !Q2 && Q4) return HighlightState.InverterQ1Q4;
            if (!Q1 && Q2 && !Q4) return HighlightState.InverterQ2;
            if (!Q1 && Q2 && Q4) return HighlightState.InverterQ2Q4;
        }

        if (Q1 && Q2 && Q4) return HighlightState.Q1Q2Q4;
        if (!Q1 && !Q2 && !Q4) return HighlightState.AllOff;
        if (Q1 && !Q2 && !Q4) return HighlightState.Q1;
        //if (q1 && !q2 && !q4 /*&& rectifier*/) return HighlightState.Q1Rectifier;
        if (Q1 && !Q2 && Q4) return HighlightState.Q1Q4;
        if (Q1 && Q2 && !Q4) return HighlightState.Q1Q2;
        if (!Q1 && Q2 && !Q4) return HighlightState.Q2;
        if (!Q1 && Q2 && Q4) return HighlightState.Q2Q4;
        if (!Q1 && !Q2 && Q4) return HighlightState.Q4;
        throw new Exception("Highlight State not found ");
    }

    public Transform[] GetTransformsToHighlight(HighlightState state){
        switch (state){
            case HighlightState.InverterQ1Q2Q4: return inverterQ1Q2Q4;
            case HighlightState.Inverter: return inverter;
            case HighlightState.InverterQ1: return inverterQ1;
            case HighlightState.InverterQ1Rectifier: return inverterQ1Rectifier;
            case HighlightState.InverterQ1Q2: return inverterQ1Q2;
            case HighlightState.InverterQ1Q4: return inverterQ1Q4;
            case HighlightState.InverterQ2: return inverterQ2;
            case HighlightState.InverterQ2Q4: return inverterQ2Q4;
            case HighlightState.Q1Q2Q4: return q1Q2Q4;
            case HighlightState.Q1: return q1;
            case HighlightState.Q1Rectifier: return q1Rectifier;
            case HighlightState.Q1Q4: return q1Q4;
            case HighlightState.Q1Q2: return q1Q2;
            case HighlightState.Q2: return q2;
            case HighlightState.Q2Q4: return q2Q4;
            case HighlightState.Q4: return q4;
            case HighlightState.AllOff: return allOff;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private Transform[] GetAllChildes(Transform[] transforms){
        var rendererList = new List<Transform>();
        foreach (var trans in transforms)
            rendererList.AddRange(trans.GetComponentsInChildren<Transform>());
        return rendererList.ToArray();
    }
}