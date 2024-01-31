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

        bool castedSpell = base.CastSpell();
        return castedSpell;
    }
    
    protected override void DoSpell()
    {
        _playerBase.AddHealth(healAmount);
    }

    private void Start() 
    {
        _playerBase = GetComponent<PlayerBase>();
    }
    
    protected override void Update()
    {
        base.Update();
    }
}