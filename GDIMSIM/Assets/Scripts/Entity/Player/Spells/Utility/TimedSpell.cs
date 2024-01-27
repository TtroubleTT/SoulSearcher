using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedSpell : SpellBase
{
    private float _spellStartTime;

    protected bool SpellActive = false;
    
    protected abstract float SpellLength { get; set; }

    protected abstract void EndSpell();

    protected virtual void Update()
    {
        if (Time.time - _spellStartTime > SpellLength && SpellActive)
        {
            SpellActive = false;
            EndSpell();
        }
    }

    public override bool CastSpell()
    {
        if (SpellActive)
            return false;
        
        if (!base.CastSpell())
            return false;

        _spellStartTime = Time.time;
        SpellActive = true;
        return true;
        // other scripts will extend this base behavior
    }
}