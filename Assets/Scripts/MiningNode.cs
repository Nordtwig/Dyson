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
	public GameController.MetalVarieties state;

	public void Start()
	{
        meshRend = GetComponent<MeshRenderer>();
        baseMaterial = meshRend.material;

		metalRandom = Random.Range(0, 10);

        if (metalRandom < 2)
		{
			state = GameController.MetalVarieties.COBALT;
		}
		else if (metalRandom < 5)
		{
			state = GameController.MetalVarieties.TUNGSTEN;
		}
		else
		{
			state = GameController.MetalVarieties.CINNABAR;
		}

		NodeMaterial();
	}

	public void NodeMaterial()
	{
		if (state == GameController.MetalVarieties.CINNABAR)
		{
            resourceValue = Random.Range(1, 4);
			foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>()) 
            {
                m.material = nodeMaterials[0];
            }
            meshRend.material = baseMaterial;
		}
		else if (state == GameController.MetalVarieties.TUNGSTEN)
		{
            resourceValue = Random.Range(2, 6);
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = nodeMaterials[1];
            }
            meshRend.material = baseMaterial;
        }
        else if (state == GameController.MetalVarieties.COBALT)
		{
            resourceValue = Random.Range(3, 9);
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = nodeMaterials[2];
            }
            meshRend.material = baseMaterial;
        }
    }

	public bool OnBoxSpawn()
    {
		resourceValue--;
        if (resourceValue == 0)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }
}
