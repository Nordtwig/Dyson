﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabricator : MonoBehaviour
{
    private Chunk.ChunkType[] chunks;

    private bool[] chunksReceived;

    [SerializeField] GameObject[] visualChunks;
    [SerializeField] Material[] visualMaterials;
    [SerializeField] GameObject[] checkMark;

    private bool restarted;

    private MiningRig miningRig;
    private Chunk chunk;
    private GameObject box;

    private void Start()
    {
        box = GameObject.Find("GetableBox");
        chunks = new Chunk.ChunkType[3];
        chunksReceived = new bool[3];
        RequiredMaterials();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chunk")
        {
            chunk = other.GetComponent<Chunk>();
            restarted = false;
            ChunkDelivered(other);
        }
    }

    public void ChunkDelivered(Collider other)
    {
        bool delivered = false;
        for (int i = 0; i < chunks.Length; i++)
        {
            if (!chunksReceived[i] && chunk.chunkType == chunks[i])
            {
                delivered = true;
                chunksReceived[i] = true;
                visualChunks[i].SetActive(false);
                checkMark[i].SetActive(true);
                Destroy(other.gameObject);
                break;
            }
        }

        GenerateBoxOrRestart(delivered);

    }

    public void GenerateBoxOrRestart(bool delivered)
    {
        int temp = 0;
        for (int i = 0; i < chunksReceived.Length; i++)
        {
            if (chunksReceived[i] == true)
            {
                temp++;
            }
        }

        if (temp == 3)
        {
            EjectBox();
            RequiredMaterials();
        }

        if (!delivered)
        {
            RequiredMaterials();
        }
    }

    private void RequiredMaterials()
    {
        restarted = true;

        for (int i = 0; i < 3; i++)
        {
            visualChunks[i].SetActive(true);
            checkMark[i].SetActive(false);
            Chunk.ChunkType randomMaterial = (Chunk.ChunkType) Random.Range(0, 3);
            chunks[i] = randomMaterial;
            chunksReceived[i] = false;

            //visuals
            if (randomMaterial == Chunk.ChunkType.CINNABAR)
            {
                visualChunks[i].GetComponent<SpriteRenderer>().material = visualMaterials[0];
            }
            else if (randomMaterial == Chunk.ChunkType.COBALT)
            {
                visualChunks[i].GetComponent<SpriteRenderer>().material = visualMaterials[2];
            }
            else
            {
                visualChunks[i].GetComponent<SpriteRenderer>().material = visualMaterials[1];
            }
        }
    }

    public void EjectBox()
    {
        GameObject go = Instantiate(box, transform.position + transform.TransformDirection(Vector3.right * 3), Quaternion.identity);
        go.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * 3 + Vector3.right * Random.Range(5, 15));
    }
}
