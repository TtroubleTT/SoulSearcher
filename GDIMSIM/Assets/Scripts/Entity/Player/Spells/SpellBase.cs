using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour
{
    protected abstract float Cooldown { get; set; }

    protected abstract void InitializeAbstractedStats();

    protected virtual bool CastSpell()
    {
        bool canCastSpell = true;

        if (!canCastSpell)
            return false;

        return true;

        // other scripts will extend this base behavior
    }
}
