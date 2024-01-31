using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpellBase : MonoBehaviour
{
    private float _lastUsed;

    private bool _cooldownActive;

    public Slider slider;
    
    protected abstract float Cooldown { get; set; }

    protected abstract void InitializeAbstractedStats();

    protected virtual void Awake()
    {
        InitializeAbstractedStats();
    }
    
    protected abstract void DoSpell();

    public virtual bool CastSpell()
    {
        if (Time.time - _lastUsed > Cooldown)
        {
            _lastUsed = Time.time;
            DoSpell();
            slider.value = 0;
            slider.maxValue = Cooldown;
            _cooldownActive = true;
            return true;
        }

        return false;
        // other scripts will extend this base behavior
    }

    public float GetCooldown()
    {
        return Cooldown;
    }

    protected virtual void Update()
    {
        if (_cooldownActive)
        {
            if (Time.time - _lastUsed < Cooldown)
            {
                slider.value = Time.time - _lastUsed;
            }
            else
            {
                _cooldownActive = false;
            }
        }
    }
}
