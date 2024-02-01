using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusEffect : MonoBehaviour
{
    public void Burning(EnemyBase enemy, float damage, int x, float delay)
    {
        StartCoroutine(BurningEffect(enemy, damage, x, delay));
    }

    private IEnumerator BurningEffect(EnemyBase enemy, float damage, int x, float delay)
    {

        for (int i = 0; i > x; i++)
        {
            enemy.SubtractHealth(damage);
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delay);

    }

    //create a new effect for freeze spell
}
