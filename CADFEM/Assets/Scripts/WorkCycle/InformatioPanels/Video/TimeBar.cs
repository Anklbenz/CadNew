using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TimeBar : MonoBehaviour {
    private TMP_Text _timeLabel;

    private void Awake(){
        _timeLabel = GetComponent<TMP_Text>();
    }

    public void Refresh(float currentTime, float totalTime){
        var currentMinutes = Mathf.Floor((int)(currentTime / 60)).ToString("00");
        var currentSeconds = ((int)currentTime % 60).ToString("00");

        var totalMinutes = Mathf.Floor((int)(totalTime / 60)).ToString("00");
        var totalSeconds = ((int)totalTime % 60).ToString("00");

        _timeLabel.text = $"{currentMinutes}:{currentSeconds} / {totalMinutes}:{totalSeconds}";
    }
}
