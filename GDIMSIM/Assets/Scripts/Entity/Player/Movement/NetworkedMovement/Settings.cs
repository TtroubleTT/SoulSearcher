using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Contributors: Taylor
    [Header("Sensitivity")] 
    [SerializeField] private float mouseXSensitivity = 300f;
    [SerializeField] private float mouseYSensitivity = 300f;
    
    [Header("Movement Settings")] 
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Spell Settings")] 
    [SerializeField] private KeyCode defaultAttackSpell = KeyCode.Mouse0;
    [SerializeField] private KeyCode firstSpell = KeyCode.E;
    [SerializeField] private KeyCode secondSpell = KeyCode.Q;
    [SerializeField] private KeyCode thirdSpell = KeyCode.R;
    [SerializeField] private KeyCode forthSpell = KeyCode.F;

    public Dictionary<PlayerControls, KeyCode> PlayerControlMap = new ();

    public enum PlayerControls
    {
        Jump,
        Sprint,
        Crouch,
        DefaultAttack,
        FirstSpell,
        SecondSpell,
        ThirdSpell,
        ForthSpell,
    }

    private void Awake()
    {
        PlayerControlMap.Add(PlayerControls.Jump, jumpKey);
        PlayerControlMap.Add(PlayerControls.Sprint, sprintKey);
        PlayerControlMap.Add(PlayerControls.Crouch, crouchKey);
        PlayerControlMap.Add(PlayerControls.DefaultAttack, defaultAttackSpell);
        PlayerControlMap.Add(PlayerControls.FirstSpell, firstSpell);
        PlayerControlMap.Add(PlayerControls.SecondSpell, secondSpell);
        PlayerControlMap.Add(PlayerControls.ThirdSpell, thirdSpell);
        PlayerControlMap.Add(PlayerControls.ForthSpell, forthSpell);
    }

    public (float, float) GetSensitivity()
    {
        return (mouseXSensitivity, mouseYSensitivity);
    }

    // Still need to impliment changing controls with menu change keycode in dictionary
    // If we add save system consider saving what people change their controls to
}
