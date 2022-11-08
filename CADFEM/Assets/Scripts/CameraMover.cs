
using System;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    [SerializeField]private  float speed = 5.0f;
    [SerializeField]private float mouseSpeed = 100f;
    private Vector3 _direction;
    private float _mouseX, _mouseY;

    private void Awake(){
        _mouseX = transform.eulerAngles.y;
        _mouseY = transform.eulerAngles.x;
    }

    private void Update(){
        var xMove = Input.GetAxis("Horizontal");
        var yMove = Input.GetAxis("Vertical");

        var moveVector = new Vector3(xMove, 0, yMove);
        transform.Translate(moveVector * Time.deltaTime * speed);

        if (!Input.GetMouseButton(0)) return;
        _mouseX += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        _mouseY += Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(-_mouseY, _mouseX, 0);
    }
}