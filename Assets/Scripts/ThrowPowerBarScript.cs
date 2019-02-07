using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Made by Ulrik
/// </summary>

public class ThrowPowerBarScript : MonoBehaviour
{
	private Slider throwPowerBar;

    // Start is called before the first frame update
    void Start()
    {
		throwPowerBar = GetComponent<Slider>();
		throwPowerBar.value = 0;
	}

	public void ThrowPowerBarUpdate(float eTime, bool holdingItem)
	{
		if (holdingItem)
		{
			throwPowerBar.value = (eTime - 0.5f) / 1f; // powerBarMax
		}
	}
}
