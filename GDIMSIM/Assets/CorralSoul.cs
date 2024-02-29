using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorralSoul : MonoBehaviour
{
    private float happinessLevel;
    [SerializeField] private float maxHappiness;
    [SerializeField] private float decreaseHappiness;
    [SerializeField] private float decreaseTime;

    // Start is called before the first frame update
    void Start()
    {
        happinessLevel = maxHappiness;
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseTime);
            happinessLevel -= decreaseHappiness;
        }
    }
}
