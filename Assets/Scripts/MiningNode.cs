using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Created by Heimer, modified by Robin, Ulrik
/// </summary>

public class MiningNode : MonoBehaviour
{
	private MeshRenderer meshRend;
	private Material baseMaterial;

    public int resourceValue;
	int metalRandom;

	// States
	public GameController.MetalVarieties materialType;

	public void Start()
	{
        meshRend = GetComponent<MeshRenderer>();
        baseMaterial = meshRend.material;

		metalRandom = Random.Range(0, 10);

        if (metalRandom < 2)
		{
			materialType = GameController.MetalVarieties.COBALT;
		}
		else if (metalRandom < 5)
		{
			materialType = GameController.MetalVarieties.TUNGSTEN;
		}
		else if (metalRandom < 9)
		{
			materialType = GameController.MetalVarieties.CINNABAR;
		}
        else
        {
			materialType = GameController.MetalVarieties.MIXED;
        }

        NodeMaterial();
	}

	public void NodeMaterial()
	{
		if (materialType == GameController.MetalVarieties.CINNABAR)
		{
            resourceValue = Random.Range(1, 4);
			foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>()) 
            {
                m.material = GameController.instance.metalMaterials[0];
            }
            meshRend.material = baseMaterial;
		}
		else if (materialType == GameController.MetalVarieties.TUNGSTEN)
		{
            resourceValue = Random.Range(2, 6);
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = GameController.instance.metalMaterials[1];
            }
            meshRend.material = baseMaterial;
        }
        else if (materialType == GameController.MetalVarieties.COBALT)
		{
            resourceValue = Random.Range(3, 9);
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = GameController.instance.metalMaterials[2];
            }
            meshRend.material = baseMaterial;
        }
        else if (materialType == GameController.MetalVarieties.MIXED)
		{
            resourceValue = Random.Range(5, 10);
            int i = 0;
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = GameController.instance.metalMaterials[i%3];
                i++;
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
