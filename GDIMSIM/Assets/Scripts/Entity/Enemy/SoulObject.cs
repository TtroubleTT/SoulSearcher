using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject soulCounter in GameObject.FindGameObjectsWithTag("SoulCounter"))
            {
                soulCounter.GetComponent<SoulCounter>().CollectSoulCount();
            }

            Destroy(gameObject);
        }
    }
}