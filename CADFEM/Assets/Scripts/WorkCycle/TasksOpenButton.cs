using System;
using UnityEngine;
using UnityEngine.UI;

public class TasksOpenButton : MonoBehaviour {
    public event Action OnClickedEvent;

    public Button button;
    private Camera _camera;

    private void Awake(){
        _camera = Camera.main;
    }

    public void Visible(bool visible){
        button.gameObject.SetActive(visible);
    }

    public void FixedUpdate(){
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }

    public void SetPosition(Vector3 position){
        transform.position = position;
    }
}