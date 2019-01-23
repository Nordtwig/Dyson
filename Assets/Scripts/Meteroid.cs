using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteroid : MonoBehaviour
{
    [SerializeField] GameObject miningNode;
    private Quaternion miningNodeSpawnRotation;
    [SerializeField] float randomNodeSpawnChance;
    private float spawnValue;

    //If meteroid collides with asteroid, spawns a miningnode according to percentage
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
            Invoke("DestroyMeteroid", 0.2f);
        }
    }

    private void DestroyMeteroid()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
