using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody rb;
    private MeshRenderer myRenderer;

    public ChunkType chunkType;


    public enum ChunkType
    {
        CINNABAR,
        TUNGSTEN,
        COBALT,
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        DetermineChunkType();
    }

    private void DetermineChunkType()
    {
        myRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        Debug.Log(myRenderer.material.name);
        if (myRenderer.material.name == "Tungsten")
        {
            chunkType = ChunkType.TUNGSTEN;
        }
        else if (myRenderer.material.name == "Cinnabar")
        {
            chunkType = ChunkType.CINNABAR;
        }
        else if (myRenderer.material.name == "Cobalt")
        {
            chunkType = ChunkType.COBALT;
        }
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
    }

    public void ThrowChunk(float throwStrength)
    {
        DropChunk();
        rb.velocity += player.rb.velocity * 2 + player.model.transform.TransformDirection(Vector3.up * 5 + Vector3.forward * 10 * throwStrength);
    }
}
