using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    //referencing the status effect script so that the spells work
    // Contributors: Taylor, Amelia

    protected bool CanAttack = true;
    
    protected List<GameObject> _playerList = new();

    public enum StatusEffect
    {
        None,
        Burning,
        Freeze,
    }

    protected override void Die()
    {
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
    }
    
    public void UpdatePlayerList()
    {
        _playerList = GameObject.FindGameObjectsWithTag("Player").ToList();
    }
    
    public void CauseStatusEffect(StatusEffect effect)
    {
        if (effect == StatusEffect.Burning)
        {
            Burning(this, 5, 3, 1f);
        }
        else if (effect == StatusEffect.Freeze)
        {
            Freeze(7f);
        }
    }

    public void Burning(EnemyBase enemy, float damage, int x, float delay)
    {
        StartCoroutine(BurningEffect(enemy, damage, x, delay));
    }
    
    public void Freeze(float delay)
    {
        StartCoroutine(FreezeEffect(delay));
    }

    private IEnumerator BurningEffect(EnemyBase enemy, float damage, int x, float delay)
    {
        for (int i = 0; i < x; i++)
        {
            enemy.SubtractHealth(damage);
            yield return new WaitForSeconds(delay);
        }
    }
    
    private IEnumerator FreezeEffect(float delay)
    {
        CanAttack = false;
        yield return new WaitForSeconds(delay);
        CanAttack = true;
    }
}
