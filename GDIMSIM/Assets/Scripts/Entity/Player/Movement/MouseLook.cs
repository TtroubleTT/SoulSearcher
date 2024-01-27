using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float _mouseXSensitivity;
    private float _mouseYSensitivity;

    [Header("References")]
    [SerializeField] private Transform playerBody; 
    [SerializeField] private Settings settings;
    private float _xRotation;

    // Code has been inspired and modified a bit based on these tutorials
    // https://www.youtube.com/watch?v=f473C43s8nE&t=505s
    // https://www.youtube.com/watch?v=_QajrabyTJc
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        (_mouseXSensitivity, _mouseYSensitivity) = settings.GetSensitivity();
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseYSensitivity * Time.deltaTime;

        // Looking up and down
        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f); // Makes it so we can only look right above us and not flip our entire camera

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f); // Its rotating about the x axis aka up and down
        
        // Looking right and left
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
