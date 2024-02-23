using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEmpty : MonoBehaviour
{
    private Corral _corral;

    private void Awake()
    {
        _corral = GetComponent<Corral>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("rahhhh");
        if (other.gameObject.CompareTag("Player"))
        {
            _corral.AddSoul(1);
        }
    }


}
