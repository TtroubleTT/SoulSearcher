using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEmpty : MonoBehaviour
{
    private SoulCounter _soulCounter;
    private Corral _corral;

    private void Awake()
    {
        _corral = gameObject.transform.parent.gameObject.GetComponent<Corral>();
        _soulCounter = FindObjectOfType<SoulCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hello");
            if (_soulCounter.GetSoulAmount() > 0)
            {
                _corral.AddSoul(1);
            }
            

        }
    }


}
