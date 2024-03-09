using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseLook : NetworkBehaviour
{
    // Contributors: Taylor
    [Header("Sensitivity")]
    // [SerializeField] private Slider mouseXSens;
    // [SerializeField] private Slider mouseYSens;
    [SerializeField] private float controllerSens;
    [SerializeField] private float keyboardSens;
    private float _mouseXSensitivity;
    private float _mouseYSensitivity;

    [Header("References")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private PlayerInput controls;
    [SerializeField] private Camera playerCamera;
    private float _xRotation;
    private Vector2 _lookInput = Vector2.zero;

    // Code has been inspired and modified a bit based on these tutorials
    // https://www.youtube.com/watch?v=f473C43s8nE&t=505s
    // https://www.youtube.com/watch?v=_QajrabyTJc
    
    private void Start()
    {
        // If the player isn't the local one they don't get that camera
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }
        
        Cursor.lockState = CursorLockMode.Locked;

        if (controls.currentControlScheme == "Controller")
        {
            _mouseXSensitivity = controllerSens;
            _mouseYSensitivity = controllerSens;
        }
        else
        {
            _mouseXSensitivity = keyboardSens;
            _mouseYSensitivity = keyboardSens;
        }
        
        // mouseXSens.value = _mouseXSensitivity;
        // mouseYSens.value = _mouseYSensitivity;
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
