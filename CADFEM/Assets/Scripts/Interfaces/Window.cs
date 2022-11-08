using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


public abstract class Window : MonoBehaviour {
[Header("Window")]
   [SerializeField] private CanvasGroup canvasGroup;
   [SerializeField] private float windowAlpha;

   public virtual void Start(){
      canvasGroup.alpha = windowAlpha;
   }

   public void Open(){
      gameObject.SetActive(true); 
   }

   public void Close(){
      gameObject.SetActive(false);
   }

   public void Show(){
      canvasGroup.alpha = windowAlpha;
      canvasGroup.interactable = true;
   }

   public void Hide(){
      canvasGroup.alpha = 0;
      canvasGroup.interactable = false;
   }
   
}
   

