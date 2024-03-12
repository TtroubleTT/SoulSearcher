using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corral : MonoBehaviour
{
    private SoulCounter _soulCounter;

    // [SerializeField] private float maxSoul = 5;
    [SerializeField] private float currentSoul = 0;

    [SerializeField] private GameObject soulObject;

    [SerializeField] private Vector3 spawnPoint;

    private void Start()
    {
        _soulCounter = FindObjectOfType<SoulCounter>();
    }


    public void AddSoul(float amount)
    {
        currentSoul = currentSoul + amount;
        Instantiate(soulObject, spawnPoint, Quaternion.identity);
        _soulCounter.DecreaseSoulCount();
    }

    public void SubtractSoul(float amount)
    {
        currentSoul = currentSoul - amount;
    }
}

