using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Popup : Window {
   protected const string CONFIRM_HEADER = "Подтвердите действие";

   [SerializeField] protected TMP_Text headerText, contentText;
   [SerializeField] protected Button acceptButton;

   
   
   protected abstract void SetAcceptResultOnClick() ;
}
