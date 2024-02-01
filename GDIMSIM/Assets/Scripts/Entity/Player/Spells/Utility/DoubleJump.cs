using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJump : TimedSpell
{
    [SerializeField] private float cooldown;
    [SerializeField] private float spellLength;
    private PlayerMovement _playerMovement;

    [SerializeField] private RawImage icon;
    
    protected override float Cooldown { get; set; }
    
    protected override float SpellLength { get; set; }
    
    protected override RawImage Icon { get; set; }

    protected override void InitializeAbstractedStats()
    {
        Cooldown = cooldown;
        SpellLength = spellLength;
        Icon = icon;
    }

    protected override void DoSpell()
    {
        _playerMovement.spellActive = true;
    }

    protected override void EndSpell()
    {
        _playerMovement.spellActive = false;
    }

    private void Start() 
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }
    
    protected override void Update()
    {
        base.Update();
    }
}