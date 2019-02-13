using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator: Heimer Ahnfelt
/// Modified by: Noah Nordqvist
/// </summary>

public class MiningNodeSpawner : MonoBehaviour
{
    [Tooltip("Amount of Nodes generated during startup")]
    [SerializeField] int initialSpawnCount = 10;
    [SerializeField, Tooltip("How many of the initial that will guaranteed spawn close to the SLS")]
    int closeSpawnCount = 3;
    [SerializeField] LayerMask asteroidLayer;
    private GameObject miningNode;
    private GameObject nodePointer;
    private GameObject nodeParent;

    void Start()
    {
        miningNode = GameObject.Find("NewMiningNode");
        nodeParent = GameObject.Find("Nodes");
        nodePointer = transform.GetChild(0).gameObject;
        SpawnNode(initialSpawnCount);
    }

    void SpawnNode(int spawnCount)
    {
        for (int i = 0; i < closeSpawnCount; i++) {
            transform.rotation = Quaternion.Euler(-40, Random.Range(0, 360f), 0);
            Vector3 direction = transform.position - nodePointer.transform.position;

            Ray ray = new Ray(nodePointer.transform.position, direction);
            RaycastHit nodeSpawnPoint;

            if (Physics.Raycast(ray, out nodeSpawnPoint, 130, asteroidLayer)) {
                GameObject myNode = Instantiate(miningNode, nodeSpawnPoint.point, Quaternion.LookRotation(Vector3.forward, Vector3.up));
                myNode.transform.SetParent(nodeParent.transform); 
            }
        }

        for (int i = closeSpawnCount; i < spawnCount; i++) {
            transform.rotation = Random.rotation;
            Vector3 direction = transform.position - nodePointer.transform.position;

            Ray ray = new Ray(nodePointer.transform.position, direction);
            RaycastHit nodeSpawnPoint;

            if (Physics.Raycast(ray, out nodeSpawnPoint, 130, asteroidLayer)) {
                Instantiate(miningNode, nodeSpawnPoint.point, Quaternion.LookRotation(Vector3.forward, Vector3.up), nodeParent.transform);
            }
        }

        //This function spawns x amount of nodes with random positions around the asteroid.
        // 1st Iteration
        //for (int i = 0; i < closeSpawnCount; i++)
        //{
        //    transform.rotation = Quaternion.Euler(-40, Random.Range(0, 360f), 0);

        //    GameObject myNode = Instantiate(miningNode, transform.position, Quaternion.Euler(-40, Random.Range(0, 360f), 0));
        //    myNode.transform.SetParent(FindObjectOfType<MiningNodeSpawner>().transform);
        //}

        //for (int i = closeSpawnCount; i < spawnCount; i++)
        //{
        //    GameObject myNode = Instantiate(miningNode, transform.position, Random.rotation);
        //    myNode.transform.SetParent(FindObjectOfType<MiningNodeSpawner>().transform);
        //}
    }
}
