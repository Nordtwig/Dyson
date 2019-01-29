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
    [SerializeField] GameObject MetroidImpactVFX;

    //If meteroid collides with asteroid, spawns a miningnode according to percentage
    [SerializeField] GameObject dangerZone;
    private GameObject zone;

	private PlayerController player;
    private GameObject[] boxes;
    private GameObject[] miningRigs;
	private Vector3 playerDistance;
	private float meteoroidHitBox = 50f;
	[Range(1, 5), SerializeField] float upForce = 2;
	[Range (10, 200), SerializeField] int sideForce = 100;
    [SerializeField] int minDistanceHurled = 10;
    [SerializeField] int maxDistanceHurled = 15;

    private Transform meteoroids;
    private bool headingTowardsSanctuary;
    private Collider sanctuaryCollider;

    private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        meteoroids = GameObject.Find("Meteoroids").transform;
        AudioManager.instance.PlayOnPos("Meteoroid Loop", transform);

        Ray ray = new Ray(transform.position, transform.parent.parent.position - transform.position);
        RaycastHit movingToSanctuary;
        if (Physics.Raycast(ray, out movingToSanctuary, (transform.position - meteoroids.position).sqrMagnitude))
        {
            if (movingToSanctuary.collider.tag == "Sanctuary")
            {
                headingTowardsSanctuary = true;
            }
        }

        RaycastHit hit;
        if (headingTowardsSanctuary == false && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            StartCoroutine(CoSpawnDangerZone(hit));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
        miningRigs = GameObject.FindGameObjectsWithTag("Rig");
        //AudioManager.instance.PlayOnPos("Meteoroid Explosion", transform);

        for (int i = 0; i < miningRigs.Length; i++)
        {
            float rigdistance = (miningRigs[i].transform.position - transform.position).sqrMagnitude;
            if (rigdistance < meteoroidHitBox)
            {
                miningRigs[i].GetComponent<MiningRig>().BreakRig();
            }
        }

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

                GameObject myVFX = Instantiate(MetroidImpactVFX, transform.position, miningNodeSpawnRotation, meteoroids);
                Destroy(myVFX, 30);
            }

            Destroy(zone);
            Invoke("DestroyMeteroid", 3f);
        }
    }

    private void DestroyMeteroid()
    {
        Destroy(transform.parent.gameObject);

    }

    private IEnumerator CoSpawnDangerZone(RaycastHit hit)
    {
        yield return new WaitForSeconds(0.1f);
        zone = Instantiate(dangerZone, hit.point, Quaternion.identity, meteoroids);
    }
}
