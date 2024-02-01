using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulCounter : MonoBehaviour
{
    public TMP_Text soulsCollected;
	void collectSoulCount()
	{
		int count = Int32.Parse(soulsCollected.text) + 1;
		soulsCollected.SetText(count.ToString());
	}
	void resetCounter()
	{
		soulsCollected.SetText("0");
	}
}
