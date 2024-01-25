using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour
{
    private float _lastUsed;
    
    protected abstract float Cooldown { get; set; }

    protected abstract void InitializeAbstractedStats();

    protected virtual void Awake()
    {
        InitializeAbstractedStats();
    }

    public virtual bool CastSpell()
    {
        if (Time.time - _lastUsed > Cooldown)
        {
            _lastUsed = Time.time;
            return true;
        }

        return false;
        // other scripts will extend this base behavior
    }
}
