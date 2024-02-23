using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEmpty : MonoBehaviour
{
    private Corral _corral;

    private void Awake()
    {
        _corral = gameObject.transform.parent.gameObject.GetComponent<Corral>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _corral.AddSoul(1);
        }
    }


}
