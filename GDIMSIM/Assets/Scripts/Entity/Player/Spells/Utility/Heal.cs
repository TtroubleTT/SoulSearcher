using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heal : SpellBase
{
    [SerializeField] private float cooldown;
    [SerializeField] private float healAmount;
    private PlayerBase _playerBase;
    
    protected override float Cooldown { get; set; }

    protected override void InitializeAbstractedStats()
    {
        Cooldown = cooldown;
    }

    public override bool CastSpell()
    {
        if (_playerBase.GetCurrentHealth() >= _playerBase.GetMaxHealth())
            return false;
        
        // Checks if Cooldown has passed
        if (!base.CastSpell())
            return false;

        DoHeal();
        return true;
    }

    private void Start() 
    {
        _playerBase = GetComponent<PlayerBase>();
    }

    private void DoHeal()
    {
        _playerBase.AddHealth(healAmount);
    }
}