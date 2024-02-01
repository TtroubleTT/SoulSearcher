using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    private EnemyStatusEffect statuseffect;

    //referencing the status effect script so that the spells work
    private void Start()
    {
        statuseffect = FindObjectOfType<EnemyStatusEffect>();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    public void CauseStatusEffect(int x)
    {
        if (x == 1)
        {
            statuseffect.Burning(this, 20, 3, 1f);
        }

    }


}
