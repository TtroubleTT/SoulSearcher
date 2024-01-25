using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour
{
    private float _lastUsed;
    protected abstract float Cooldown { get; set; }

    protected abstract void InitializeAbstractedStats();

    public virtual bool CastSpell()
    {
        if (Time.time - _lastUsed > Cooldown)
        {
            return true;
        }

        return false;
        // other scripts will extend this base behavior
    }
}
