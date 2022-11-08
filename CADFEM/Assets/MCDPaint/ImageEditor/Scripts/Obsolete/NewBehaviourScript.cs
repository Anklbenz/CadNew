using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    private Camera renderCamera;
    private LayerMask drawingCanvasLayerMask;
    
    [SerializeField]  GraphicRaycaster m_Raycaster;
    [SerializeField] EventSystem m_EventSystem;
    PointerEventData m_PointerEventData;
    
    private Vector3? GetTouchWordPosition(Vector2 touchPos){
        const float rayLength = 100f;
        var ray = renderCamera.ScreenPointToRay(touchPos);
        
        if (Physics.Raycast(ray, out var raycastHit, rayLength, drawingCanvasLayerMask))
            return raycastHit.point;

        return null;
    }
    
    private void GetUIObjectsOfPosition(Vector2 screenPosition)
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (var result in results) {
            Debug.Log(result.gameObject.name);
        }               
    }

    private void Get(){
        m_PointerEventData = new PointerEventData(m_EventSystem)
        {
            //Set the Pointer Event Position to that of the mouse position
            position = Input.mousePosition
        };

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (var result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }
    }
}
