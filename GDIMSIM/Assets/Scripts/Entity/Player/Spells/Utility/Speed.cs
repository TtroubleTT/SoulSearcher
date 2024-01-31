using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Speed : TimedSpell
{
    [SerializeField] private float cooldown;
    [SerializeField] private float spellLength;
    [SerializeField] private float amountIncreased;
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
        _playerMovement.UpdateSpeed(amountIncreased);
    }

    protected override void EndSpell()
    {
        _playerMovement.UpdateSpeed(-amountIncreased);
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