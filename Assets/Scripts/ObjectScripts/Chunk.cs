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
    public override void PickUpItem()
    {
        base.PickUpItem();
        if (chunkType == GameController.MetalVarieties.CINNABAR)
        {
            player.SetEnableHoldingChunkCinnabar(true);
        }
        else if (chunkType == GameController.MetalVarieties.TUNGSTEN)
        {
            player.SetEnableHoldingChunkTungsten(true);
        }
        else if (chunkType == GameController.MetalVarieties.COBALT)
        {
            player.SetEnableHoldingChunkCobalt(true);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            AudioManager.instance.PlayOnPos("Chunk Collision", transform);
        }
    }
}
