using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulObject : MonoBehaviour
{
    // Contributors: Taylor
    private SoulCounter _soulCounter;
    private void Awake()
    {
        _soulCounter = GameObject.FindGameObjectWithTag("SoulCounter").GetComponent<SoulCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _soulCounter.CollectSoulCount();
            Destroy(gameObject);
        }
    }
}