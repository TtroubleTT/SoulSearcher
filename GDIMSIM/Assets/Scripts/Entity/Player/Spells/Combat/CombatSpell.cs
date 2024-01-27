using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatSpell : SpellBase, ICombat
{
    public abstract float Damage { get; set; }

    public bool HurtEnemy(GameObject enemy, float damage)
    {
        if (!enemy.CompareTag("Enemy"))
            return false;
        
        enemy.GetComponent<EnemyBase>().SubtractHealth(damage);
        return true;
    }
}