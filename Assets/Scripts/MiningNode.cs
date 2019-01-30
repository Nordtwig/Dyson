using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Created by Heimer, modified by Robin, Ulrik
/// </summary>

public class MiningNode : MonoBehaviour
{
	[SerializeField] private Material[] nodeMaterials;
	private Material baseMaterial;

	private MeshRenderer meshRend;

	public int resourceValue;
	int metalRandom;

	// States
	public MetalVarieties state = MetalVarieties.NULL;

	public enum MetalVarieties
	{
		NULL = 1,
		CINNABAR,
		TUNGSTEN,
		COBALT,
	}

	public void Start()
	{
        meshRend = GetComponent<MeshRenderer>();
        baseMaterial = meshRend.material;

		metalRandom = Random.Range(0, 10);

        if (metalRandom < 2)
		{
			state = MetalVarieties.COBALT;
		}
		else if (metalRandom < 5)
		{
			state = MetalVarieties.TUNGSTEN;
		}
		else
		{
			state = MetalVarieties.CINNABAR;
		}

		NodeMaterial();
	}

	public void NodeMaterial()
	{
		if (state == MetalVarieties.CINNABAR)
		{
			foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = nodeMaterials[0];
            }
            meshRend.material = baseMaterial;
		}
		else if (state == MetalVarieties.TUNGSTEN)
		{
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = nodeMaterials[1];
            }
            meshRend.material = baseMaterial;
        }
        else if (state == MetalVarieties.COBALT)
		{
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = nodeMaterials[2];
            }
            meshRend.material = baseMaterial;
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
