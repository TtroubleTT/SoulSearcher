using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulCounter : MonoBehaviour
{
	// Contributors: Taylor, Richard
    public TMP_Text soulsCollected;

    private float _soulAmount = 0;

	public float GetSoulAmount()
	{
		return _soulAmount;
	}
    
	public void CollectSoulCount()
	{
		_soulAmount++;
		soulsCollected.SetText($"{_soulAmount} / 10");
	}

    public void DecreaseSoulCount()
    {
        _soulAmount--;
        soulsCollected.SetText($"{_soulAmount} / 10");
    }

    public void ResetCounter()
	{
		soulsCollected.SetText("0 / 10");
	}

}
