using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cube : MonoBehaviour
{
    private ControllerInputs _controllerInputs;

    private Vector2 _move;
    private Vector2 _rotate;

    private void Awake()
    {
        _controllerInputs = new ControllerInputs();
        
        _controllerInputs.Gameplay.Move.performed += ctx => _move = ctx.ReadValue<Vector2>();
        _controllerInputs.Gameplay.Move.canceled += ctx => _move = Vector2.zero;

        _controllerInputs.Gameplay.Rotate.performed += ctx => _rotate = ctx.ReadValue<Vector2>();
        _controllerInputs.Gameplay.Rotate.canceled += ctx => _rotate = Vector2.zero;
    }

    private void Update()
    {
        Vector3 m = new Vector3(_move.x, 0, _move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);

        Vector2 r = new Vector2(-_rotate.y, _rotate.x) * (100f * Time.deltaTime);
        transform.Rotate(r, Space.World);
    }

    private void OnEnable()
    {
        _controllerInputs.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _controllerInputs.Gameplay.Disable();
    }
}
