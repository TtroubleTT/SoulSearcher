using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    // Contributors: Taylor
    [Header("Sensitivity")] 
    [SerializeField] private Slider mouseXSens;
    [SerializeField] private Slider mouseYSens;
    private float _mouseXSensitivity;
    private float _mouseYSensitivity;

    [Header("References")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private PlayerInput controls;
    private float _xRotation;
    private Vector2 _lookInput = Vector2.zero;

    // Code has been inspired and modified a bit based on these tutorials
    // https://www.youtube.com/watch?v=f473C43s8nE&t=505s
    // https://www.youtube.com/watch?v=_QajrabyTJc
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (controls.currentControlScheme == "Controller")
        {
            _mouseXSensitivity = 300;
            _mouseYSensitivity = 300;
        }
        else
        {
            _mouseXSensitivity = 20;
            _mouseYSensitivity = 20;
        }
        
        mouseXSens.value = _mouseXSensitivity;
        mouseYSens.value = _mouseYSensitivity;
    }
    
    private void Update()
    {
        float mouseX = _lookInput.x * _mouseXSensitivity * Time.deltaTime;
        float mouseY = _lookInput.y * _mouseYSensitivity * Time.deltaTime;

        // Looking up and down
        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f); // Makes it so we can only look right above us and not flip our entire camera

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f); // Its rotating about the x axis aka up and down
        
        // Looking right and left
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }
}
