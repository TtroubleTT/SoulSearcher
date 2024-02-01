using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("adding soul");
            Destroy(gameObject);
        }
    }
}