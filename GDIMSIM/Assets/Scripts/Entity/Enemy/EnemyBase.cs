using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    protected virtual float SoulDropAmount { get; set; } = 10;

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
