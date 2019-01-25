﻿using System.Collections;
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
    [SerializeField] GameObject MetroidImpactVFX;

    //If meteroid collides with asteroid, spawns a miningnode according to percentage
    [SerializeField] GameObject dangerZone;
    private GameObject zone;

	private PlayerController player;
    private GameObject[] boxes;
	private Vector3 playerDistance;
	private float meteoroidHitBox = 50f;
	[Range(1, 5), SerializeField] float upForce = 2;
	[Range (10, 200), SerializeField] int sideForce = 100;
    [SerializeField] int minDistanceHurled = 10;
    [SerializeField] int maxDistanceHurled = 15;

    private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            zone = Instantiate(dangerZone, hit.point, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        for (int i = 0; i < boxes.Length; i++)
        {
            float boxDistance = (boxes[i].transform.position - transform.position).sqrMagnitude;
            if (boxDistance < meteoroidHitBox)
            {
                Destroy(boxes[i]);
            }
        }

        if (other.tag == "Asteroid")
        {
			playerDistance = player.GetComponent<Transform>().position - transform.position;
			if (playerDistance.sqrMagnitude < meteoroidHitBox)
			{
				player.rb.velocity = player.transform.TransformDirection(Vector3.up * upForce) + (playerDistance.normalized / Mathf.Clamp(playerDistance.sqrMagnitude, minDistanceHurled, maxDistanceHurled)) * sideForce;
			}

			spawnValue = Random.Range(0f, 1f);
            if (spawnValue <= randomNodeSpawnChance)
            {
                miningNodeSpawnRotation = gameObject.GetComponentInParent<Transform>().rotation;
                GameObject myNode = Instantiate(miningNode, miningNode.transform.position, miningNodeSpawnRotation);
                myNode.transform.SetParent(FindObjectOfType<MiningNodeSpawner>().transform);

                Instantiate(MetroidImpactVFX, transform.position, miningNodeSpawnRotation);
            }
            Destroy(zone);
            Invoke("DestroyMeteroid", 0.2f);
        }
    }

    private void DestroyMeteroid()
    {
        Destroy(transform.parent.gameObject);
    }
}
