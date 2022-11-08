using System;
using UnityEngine;

public class InputHandler : MonoBehaviour {
    public event Action OnLeftMouseButtonEvent;
    public event Action<Vector2> OnLeftMouseButtonWithPosEvent;
    public event Action OnLeftMouseButtonUpEvent;

    public Vector2 MousePosition => Input.mousePosition;

    void Update()
    {
        
        if (Input.GetMouseButton(0)){
            OnLeftMouseButtonEvent?.Invoke();
            OnLeftMouseButtonWithPosEvent?.Invoke(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
            OnLeftMouseButtonUpEvent?.Invoke();
    }
}
