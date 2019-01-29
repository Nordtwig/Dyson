using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Created by Heimer, modified by Robin, Ulrik
/// </summary>

public class MiningNode : MonoBehaviour
{
	[SerializeField] private GameObject nodeMaterial;

	private MeshRenderer meshRend;

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
        //meshRend = nodeMaterial.GetComponent<MeshRenderer>();

        if (metalRandom < 2)
		{
			state = MetalVarieties.GOLD;
		}
		else if (metalRandom < 5)
		{
			state = MetalVarieties.SILVER;
		}
		else
		{
			state = MetalVarieties.COPPER;
		}

		NodeMaterial();
	}

	public void NodeMaterial()
	{
		if (state == MetalVarieties.GOLD)
		{
			// Get material
		}
		else if (state == MetalVarieties.SILVER)
		{
			// Get material
		}
		else if (state == MetalVarieties.COPPER)
		{
			// Get material
		}
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
