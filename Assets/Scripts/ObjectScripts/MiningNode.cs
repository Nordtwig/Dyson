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
    public int resourceValue;
    GameObject standardModel;
    GameObject mixedModel;
	int metalRandom;

	// States
	public GameController.MetalVarieties materialType;

	public void Start()
	{
        standardModel = transform.GetChild(0).gameObject;
        mixedModel = transform.GetChild(1).gameObject;
        standardModel.SetActive(false);
        mixedModel.SetActive(false);

        transform.rotation = UnityEngine.Random.rotation;

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
        int mixedCount = 0;

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
                case GameController.MetalVarieties.MIXED:
                    mixedCount++;
                    break;
            }
        }

        if (cobaltCount < 1) 
        {
            return 1;
        }
        if (tungstenCount < 1) 
        {
            return 3;
        }
        if (cinnabarCount < 1) 
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
            resourceValue = UnityEngine.Random.Range(2, 5);
            standardModel.SetActive(true);
			transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = GameController.instance.metalMaterials[0];
		}
		else if (materialType == GameController.MetalVarieties.TUNGSTEN)
		{
            resourceValue = UnityEngine.Random.Range(3, 7);
            standardModel.SetActive(true);
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = GameController.instance.metalMaterials[1];
        }
        else if (materialType == GameController.MetalVarieties.COBALT)
		{
            resourceValue = UnityEngine.Random.Range(3, 9);
            standardModel.SetActive(true);
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = GameController.instance.metalMaterials[2];
        }
        else if (materialType == GameController.MetalVarieties.MIXED)
		{
            resourceValue = UnityEngine.Random.Range(5, 11);
            mixedModel.SetActive(true);
            int i = 0;
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
