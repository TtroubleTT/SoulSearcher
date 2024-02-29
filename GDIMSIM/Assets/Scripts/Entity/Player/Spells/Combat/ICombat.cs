using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    // Contributors: Taylor
    public float Damage { get; set; }

    public bool HurtEnemy(GameObject enemy);
}