using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : SpellBase
{
    protected override float Cooldown { get; set; }

    protected override void InitializeAbstractedStats()
    {
        
    }

    public override bool CastSpell()
    {
        bool canCastSpell = true;

        if (!canCastSpell)
            return false;

        return true;

        // other scripts will extend this base behavior
    }
}