using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator: Heimer
/// </summary>

public class MiningNodeSpawner : MonoBehaviour
{
    [Tooltip("Amount of Nodes generated during startup")]
    [SerializeField] int initialSpawnCount = 10;
    [Tooltip("miningNodeCenterpoint prefab goes here")]
    [SerializeField] GameObject miningNode;


    void Start()
    {
        SpawnNode(initialSpawnCount);
    }

    //This function spawns x amount of nodes with random positions around the asteroid.
    void SpawnNode(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(miningNode, transform.position, Random.rotation);
        }
    }
}
