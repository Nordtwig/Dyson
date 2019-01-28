using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Created by Heimer, modified by Robin, Ulrik
/// </summary>

public class MiningNode : MonoBehaviour
{
	public int resourceValue;
	int metalRandom;

	// States
	public MetalVarieties state = MetalVarieties.NULL;

	public enum MetalVarieties
	{
		NULL = 1,
		GOLD,
		SILVER,
		COPPER,
	}

	public void Start()
	{
		metalRandom = Random.Range(1, 10);

		if (metalRandom < 2)
		{
			state = MetalVarieties.GOLD;
		}
		else if (metalRandom < 5)
		{
			state = MetalVarieties.SILVER;
		}
		else if (metalRandom < 11)
		{
			state = MetalVarieties.COPPER;
		}
	}

	public void randomForMetal()
	{

	}

	public bool OnBoxSpawn()
    {
		resourceValue = Random.Range(1, 5);
		resourceValue--;
        if (resourceValue == 0)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }
}
