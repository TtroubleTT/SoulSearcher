using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Sensitivity")] 
    [SerializeField] private float mouseXSensitivity = 300f;
    [SerializeField] private float mouseYSensitivity = 300f;
    
    [Header("Player Settings")] 
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    private Dictionary<PlayerControls, KeyCode> _playerControlMap = new ();

    public enum PlayerControls
    {
        Jump,
        Sprint,
        Crouch,
    }

    private void Awake()
    {
        _playerControlMap.Add(PlayerControls.Jump, jumpKey);
        _playerControlMap.Add(PlayerControls.Sprint, sprintKey);
        _playerControlMap.Add(PlayerControls.Crouch, crouchKey);
    }

    public (float, float) GetSensitivity()
    {
        return (mouseXSensitivity, mouseYSensitivity);
    }

    public Dictionary<PlayerControls, KeyCode> GetPlayerControls()
    {
        return _playerControlMap;
    }
}
