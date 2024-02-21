using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corral : MonoBehaviour
{
    [SerializeField] private float MaxSoul = 5;
    [SerializeField] private float CurrentSoul = 0;

    public void AddSoul(float amount)
    {
        CurrentSoul = CurrentSoul + amount;
    }

    public void SubtractSoul(float amount)
    {
        CurrentSoul = CurrentSoul - amount;
    }
}

