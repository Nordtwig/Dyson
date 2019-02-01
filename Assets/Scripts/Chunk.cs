using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private PlayerController player;

    public Rigidbody rb;
    public GameController.MetalVarieties chunkType;
    public MeshRenderer myRenderer;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

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
        transform.SetParent(player.transform);
        gameObject.SetActive(false);
    }

    //Chunk is unparented, reactivated and placed in front of player, player holdingItem bool is set to false
    public void DropChunk()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        transform.position = player.transform.position + player.model.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 4);
        rb.velocity = Vector3.zero;
    }

    public void ThrowChunk(float throwStrength)
    {
        DropChunk();
        rb.velocity = player.transform.TransformDirection(Vector3.forward * player.playerSpeed) + player.model.transform.TransformDirection(Vector3.up * 2 + Vector3.forward * 5 * throwStrength);
    }
}
