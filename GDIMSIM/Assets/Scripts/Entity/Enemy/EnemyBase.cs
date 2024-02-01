using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    //referencing the status effect script so that the spells work

    protected bool CanAttack = true;

    private SoulCounter _soulCounter;
    
    public enum StatusEffect
    {
        None,
        Burning,
        Freeze,
    }
    
    protected override void Awake()
    {
        base.Awake();
        _soulCounter = GameObject.FindGameObjectWithTag("SoulCounter").GetComponent<SoulCounter>();
    }

    protected override void Die()
    {
        _soulCounter.CollectSoulCount();
        Destroy(gameObject);
    }

    public void CauseStatusEffect(StatusEffect effect)
    {
        if (effect == StatusEffect.Burning)
        {
            Burning(this, 5, 3, 1f);
        }
        else if (effect == StatusEffect.Freeze)
        {
            Freeze(7, 1f);
        }
    }

    public void Burning(EnemyBase enemy, float damage, int x, float delay)
    {
        StartCoroutine(BurningEffect(enemy, damage, x, delay));
    }
    
    public void Freeze(int x, float delay)
    {
        StartCoroutine(FreezeEffect( x, delay));
    }

    private IEnumerator BurningEffect(EnemyBase enemy, float damage, int x, float delay)
    {
        for (int i = 0; i < x; i++)
        {
            enemy.SubtractHealth(damage);
            yield return new WaitForSeconds(delay);
        }
    }
    
    private IEnumerator FreezeEffect(int x, float delay)
    {
        CanAttack = false;
        yield return new WaitForSeconds(delay);
        CanAttack = true;
    }
}
