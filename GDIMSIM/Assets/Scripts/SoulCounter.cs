using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro

public class SoulCounter : MonoBehaviour
{
    public TMP_Text soulsCollected;
	void collectSoulCount()
	{
		soulsCollected.SetText(int(soulsCollected) + 1);
	}
	void resetCounter()
	{
		soulsCollected.SetText(int(0));
	}
}
