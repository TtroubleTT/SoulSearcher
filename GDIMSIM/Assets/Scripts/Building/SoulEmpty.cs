using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEmpty : MonoBehaviour
{
    private Corral corral;

    private void Awake()
    {
        corral = GetComponent<Corral>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("rahhhh");
        if (other.gameObject.CompareTag("Player"))
        {
            
            corral.AddSoul(1);
        }
    }


}
