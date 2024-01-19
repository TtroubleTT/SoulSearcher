using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    protected abstract float SoulCost { get; set; }

    protected abstract void InitializeAbstractedStats();

    protected virtual bool DoAbility()
    {
        bool canDoAbility = true;

        if (!canDoAbility)
            return false;

        return true;

        // other scripts will extend this base behavior
    }
}
