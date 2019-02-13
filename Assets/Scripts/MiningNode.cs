using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Created by: Heimer
///  Modified by: Robin, Ulrik, Noah
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

        metalRandom = CalculateNodeType();

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

        GameController.instance.UpdateNodeTypes();
    }

    private int CalculateNodeType() 
    {
        int cobaltCount = 0;
        int tungstenCount = 0;
        int cinnabarCount = 0;

        foreach (GameObject node in GameController.instance.nodes) 
        {
            switch (node.GetComponent<MiningNode>().materialType) 
            {
                case GameController.MetalVarieties.COBALT:
                    cobaltCount++;
                    break;
                case GameController.MetalVarieties.TUNGSTEN:
                    tungstenCount++;
                    break;
                case GameController.MetalVarieties.CINNABAR:
                    cinnabarCount++;
                    break;
            }
        }

        if (cobaltCount < 2) 
        {
            return 1;
        }
        if (tungstenCount < 2) 
        {
            return 3;
        }
        if (cinnabarCount < 2) 
        {
            return 7;
        }
        else 
        {
            return UnityEngine.Random.Range(0, 10);
        }
    }

    public void NodeMaterial()
	{
		if (materialType == GameController.MetalVarieties.CINNABAR)
		{
            resourceValue = UnityEngine.Random.Range(1, 4);
			foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>()) 
            {
                m.material = GameController.instance.metalMaterials[0];
            }
            meshRend.material = baseMaterial;
		}
		else if (materialType == GameController.MetalVarieties.TUNGSTEN)
		{
            resourceValue = UnityEngine.Random.Range(2, 6);
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = GameController.instance.metalMaterials[1];
            }
            meshRend.material = baseMaterial;
        }
        else if (materialType == GameController.MetalVarieties.COBALT)
		{
            resourceValue = UnityEngine.Random.Range(3, 9);
            foreach (MeshRenderer m in transform.GetComponentsInChildren<MeshRenderer>())
            {
                m.material = GameController.instance.metalMaterials[2];
            }
            meshRend.material = baseMaterial;
        }
        else if (materialType == GameController.MetalVarieties.MIXED)
		{
            resourceValue = UnityEngine.Random.Range(5, 10);
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
            GameController.instance.nodes.Remove(gameObject);
            GameController.instance.UpdateNodeTypes();
            Destroy(gameObject);
            return false;
        }
        return true;
    }
}
