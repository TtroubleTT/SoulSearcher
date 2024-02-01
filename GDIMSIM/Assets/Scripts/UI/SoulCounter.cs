using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulCounter : MonoBehaviour
{
    public TMP_Text soulsCollected;
    
	public void CollectSoulCount()
	{
		int count = Int32.Parse(soulsCollected.text) + 1;
		soulsCollected.SetText($"{count} / 10");
	}
	
	public void ResetCounter()
	{
		soulsCollected.SetText("0 / 10");
	}
}
