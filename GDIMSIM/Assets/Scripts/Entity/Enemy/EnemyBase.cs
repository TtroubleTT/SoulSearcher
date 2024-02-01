using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    //referencing the status effect script so that the spells work
    
    public enum StatusEffect
    {
        None,
        Burning,
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    public void CauseStatusEffect(StatusEffect effect)
    {
        if (effect == StatusEffect.Burning)
        {
            Burning(this, 5, 3, 1f);
        }
    }

    public void Burning(EnemyBase enemy, float damage, int x, float delay)
    {
        StartCoroutine(BurningEffect(enemy, damage, x, delay));
    }

    private IEnumerator BurningEffect(EnemyBase enemy, float damage, int x, float delay)
    {
        for (int i = 0; i < x; i++)
        {
            enemy.SubtractHealth(damage);
            yield return new WaitForSeconds(delay);
        }
    }
}
