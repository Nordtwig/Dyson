using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Created by Ulrik, Heavily modified by Robin now works of pickupable object SuperClass
/// </summary>
public class Chunk : PickupableObject
{
    public GameController.MetalVarieties chunkType;
    public MeshRenderer myRenderer;

    public void RandomChunkType()
    {
        int randomRoll = Random.Range(0, 3);
        if (randomRoll == 0)
        {
            chunkType = GameController.MetalVarieties.CINNABAR;
        }
        else if (randomRoll == 1)
        {
            chunkType = GameController.MetalVarieties.TUNGSTEN;
        }
        else 
        {
            chunkType = GameController.MetalVarieties.COBALT;
        }
        myRenderer.material = GameController.instance.metalMaterials[randomRoll];
    }

    //Chunk is parented to player and disabled for transport, player holdingItem bool sets to true
    public void PickUpChunk()
    {
        player.GetComponent<PlayerController>().SetHoldingItem(true);
        transform.SetParent(player.transform);
        gameObject.SetActive(false);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //play sound
    }
}
