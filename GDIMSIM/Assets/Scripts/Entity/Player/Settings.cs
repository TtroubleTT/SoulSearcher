using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Sensitivity")] 
    [SerializeField] private float mouseXSensitivity = 300f;
    [SerializeField] private float mouseYSensitivity = 300f;
    
    [Header("Movement Settings")] 
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Spell Settings")] 
    [SerializeField] private KeyCode firstSpell = KeyCode.E;
    [SerializeField] private KeyCode secondSpell = KeyCode.Q;
    [SerializeField] private KeyCode thirdSpell = KeyCode.R;
    [SerializeField] private KeyCode forthSpell = KeyCode.F;

    private Dictionary<PlayerControls, KeyCode> _playerControlMap = new ();

    public enum PlayerControls
    {
        Jump,
        Sprint,
        Crouch,
        FirstSpell,
        SecondSpell,
        ThirdSpell,
        ForthSpell,
    }

    private void Awake()
    {
        _playerControlMap.Add(PlayerControls.Jump, jumpKey);
        _playerControlMap.Add(PlayerControls.Sprint, sprintKey);
        _playerControlMap.Add(PlayerControls.Crouch, crouchKey);
        _playerControlMap.Add(PlayerControls.FirstSpell, firstSpell);
        _playerControlMap.Add(PlayerControls.SecondSpell, secondSpell);
        _playerControlMap.Add(PlayerControls.ThirdSpell, thirdSpell);
        _playerControlMap.Add(PlayerControls.ForthSpell, forthSpell);
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
