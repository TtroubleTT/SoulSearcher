using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedSpell : SpellBase
{
    protected abstract float SpellLength { get; set; }
}