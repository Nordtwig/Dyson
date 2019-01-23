using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator Petter, Modified by Heimer
/// </summary>

public class Meteroid : MonoBehaviour
{
    //If meteroid collides with asteroid, spawns a miningnode according to percentage
    [SerializeField] GameObject miningNode;
    private Quaternion miningNodeSpawnRotation;
    [SerializeField] float randomNodeSpawnChance;
    private float spawnValue;

    //If meteroid collides with asteroid, spawns a miningnode according to percentage
    [SerializeField] GameObject dangerZone;
    private GameObject zone;

    private void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            zone = Instantiate(dangerZone, hit.point, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            spawnValue = Random.Range(0f, 1f);
            if (spawnValue <= randomNodeSpawnChance)
            {
                miningNodeSpawnRotation = gameObject.GetComponentInParent<Transform>().rotation;
                Instantiate(miningNode, miningNode.transform.position, miningNodeSpawnRotation);
            }
            Destroy(zone);
            Invoke("DestroyMeteroid", 0.2f);
        }
    }

    private void DestroyMeteroid()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
