using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Speed : TimedSpell
{
    [SerializeField] private float cooldown;
    [SerializeField] private float spellLength;
    [SerializeField] private float amountIncreased;
    private PlayerMovement _playerMovement;
    
    protected override float Cooldown { get; set; }
    
    protected override float SpellLength { get; set; }

    protected override void InitializeAbstractedStats()
    {
        Cooldown = cooldown;
        SpellLength = spellLength;
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
}