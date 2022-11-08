using UnityEngine;

public class LookAtRotator  {
   private readonly Camera _cam;
   private readonly RectTransform[] _lookAts;

   public LookAtRotator(RectTransform[] lookAts ){
      _cam = Camera.main;
      _lookAts = lookAts;
   }

   public void LookAtCamera(){
      foreach(var lookAt in _lookAts)
         lookAt.transform.rotation = Quaternion.LookRotation(lookAt.transform.position - _cam.transform.position);
   }

   public void LookToDefaultPosition(){
      foreach (var lookAt in _lookAts)
         lookAt.gameObject.transform.localRotation = Quaternion.identity; 
   }
}