using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatSpell : SpellBase, ICombat
{
    public abstract float Damage { get; set; }

    public virtual bool HurtEnemy(GameObject enemy)
    {
        if (!enemy.CompareTag("Enemy"))
            return false;
        
        enemy.GetComponent<EnemyBase>().SubtractHealth(Damage);
        return true;
    }
}