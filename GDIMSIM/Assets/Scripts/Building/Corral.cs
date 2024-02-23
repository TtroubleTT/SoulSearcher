using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corral : MonoBehaviour
{
    // [SerializeField] private float maxSoul = 5;
    [SerializeField] private float currentSoul = 0;

    public void AddSoul(float amount)
    {
        currentSoul = currentSoul + amount;
    }

    public void SubtractSoul(float amount)
    {
        currentSoul = currentSoul - amount;
    }
}

