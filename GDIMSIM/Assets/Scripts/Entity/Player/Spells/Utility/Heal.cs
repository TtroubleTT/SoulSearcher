using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Heal : SpellBase
{
    [SerializeField] private float cooldown;
    [SerializeField] private float healAmount;
    private PlayerBase _playerBase;

    [SerializeField] private RawImage icon;
    
    protected override float Cooldown { get; set; }
    
    protected override RawImage Icon { get; set; }

    protected override void InitializeAbstractedStats()
    {
        Cooldown = cooldown;
        Icon = icon;
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