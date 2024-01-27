using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedSpell : SpellBase
{
    protected abstract float SpellLength { get; set; }
    
    public override bool CastSpell()
    {
        if (Time.time - _lastUsed > Cooldown)
        {
            _lastUsed = Time.time;
            DoSpell();
            return true;
        }

        return false;
        // other scripts will extend this base behavior
    }
}