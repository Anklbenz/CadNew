using Cysharp.Threading.Tasks;
using RequestParamClasses;
using UnityEngine;
using ClassesForJsonDeserialize;
using UnityEngine.UI;

public class ControlParamsInputPanel : Window {
   [Header("ControlParams")]
   [SerializeField] protected RectTransform paramsPanelTransform;

   [SerializeField] protected RectTransform paramsItemsParent;
   [SerializeField] private Button acceptButton, closeButton;

   [Header("CPMenuItems")]
   [SerializeField] protected MenuItemInput menuItemInputPrefab;

   [SerializeField] protected MenuItemDropDown menuItemDropDownPrefab;
   [SerializeField] protected MenuItemState menuItemStatePrefab;



   [SerializeField] protected Image iconPreviewImage, fullPreviewImage;
   [SerializeField] private Button iconPreviewButton, fullPreviewButton, takePhotoButton, deletePhotoButton;

   private UniTaskCompletionSource<UserEnteredParams> _controlParamsCompletionSource;

   protected virtual void Awake(){
      var itemFactory = new MenuItemFactory(menuItemInputPrefab, menuItemStatePrefab, menuItemDropDownPrefab, paramsItemsParent);
     
   }


   protected virtual void Initialize(){
      
   }
  
   protected virtual void OnCloseClicked(){}
   
}