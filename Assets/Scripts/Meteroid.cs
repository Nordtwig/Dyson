using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator Petter, Modified by Heimer
/// </summary>

public class Meteroid : MonoBehaviour
{
    //If meteroid collides with asteroid, spawns a miningnode according to percentage
    private Quaternion miningNodeSpawnRotation;
    private float spawnValue;
    [SerializeField] GameObject MetroidImpactVFX;
    [SerializeField] GameObject miningNode;
    [SerializeField] float randomNodeSpawnChance = 0.1f;

    //If meteroid collides with asteroid, spawns a miningnode according to percentage
    [SerializeField] GameObject dangerZone;
    private GameObject zone;

	private PlayerController player;
    private GameObject[] boxes;
    private GameObject[] miningRigs;
	private Vector3 playerDistance;
	private float meteoroidHitBox = 7f;
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
        miningNode = GameObject.Find("GetableMiningNode");
        dangerZone = GameObject.Find("GetableDangerZone");
        AudioManager.instance.PlayOnPos("Meteoroid Loop", transform);

        Ray ray = new Ray(transform.position, transform.parent.position - transform.position);
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
        //boxes = GameObject.FindGameObjectsWithTag("Box");
        //miningRigs = GameObject.FindGameObjectsWithTag("Rig");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meteoroidHitBox);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Box")
            {
                Vector3 boxDistance = hitColliders[i].GetComponent<Transform>().position - transform.position;
                hitColliders[i].GetComponent<Rigidbody>().velocity = hitColliders[i].transform.TransformDirection(Vector3.up * upForce) + (boxDistance.normalized / Mathf.Clamp(boxDistance.sqrMagnitude, minDistanceHurled, maxDistanceHurled)) * sideForce;
            }

            if (hitColliders[i].tag == "Player")
            {
                playerDistance = player.GetComponent<Transform>().position - transform.position;
                player.rb.velocity = player.transform.TransformDirection(Vector3.up * upForce) + (playerDistance.normalized / Mathf.Clamp(playerDistance.sqrMagnitude, minDistanceHurled, maxDistanceHurled)) * sideForce;
            }

            if (hitColliders[i].tag == "Rig")
            {
                hitColliders[i].GetComponent<MiningRig>().BreakRig();
            }
        }

        /*for (int i = 0; i < miningRigs.Length; i++)
        {
            float rigdistance = (miningRigs[i].transform.position - transform.position).sqrMagnitude;
            if (rigdistance < meteoroidHitBox)
            {
                miningRigs[i].GetComponent<MiningRig>().BreakRig();
            }
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            Vector3 boxDistance = boxes[i].GetComponent<Transform>().position - transform.position;
            if (boxDistance.sqrMagnitude < meteoroidHitBox)
            {
                boxes[i].GetComponent<Rigidbody>().velocity = boxes[i].transform.TransformDirection(Vector3.up * upForce) + (boxDistance.normalized / Mathf.Clamp(boxDistance.sqrMagnitude, minDistanceHurled, maxDistanceHurled)) * sideForce;
            }
        }*/

        if (other.tag == "Asteroid")
        {
			/*playerDistance = player.GetComponent<Transform>().position - transform.position;
			if (playerDistance.sqrMagnitude < meteoroidHitBox)
			{
				player.rb.velocity = player.transform.TransformDirection(Vector3.up * upForce) + (playerDistance.normalized / Mathf.Clamp(playerDistance.sqrMagnitude, minDistanceHurled, maxDistanceHurled)) * sideForce;
			}*/

			spawnValue = Random.Range(0f, 1f);
            if (spawnValue <= randomNodeSpawnChance)
            {
                miningNodeSpawnRotation = gameObject.GetComponentInParent<Transform>().rotation;
                GameObject myNode = Instantiate(miningNode, Vector3.zero, miningNodeSpawnRotation);
                myNode.transform.SetParent(FindObjectOfType<MiningNodeSpawner>().transform);

                GameObject nodeSpawnedVFX = Instantiate(MetroidImpactVFX, transform.position, miningNodeSpawnRotation, meteoroids);
                Destroy(nodeSpawnedVFX, 30);
            }
            else
            {
                GameObject groundImpactVFX = Instantiate(MetroidImpactVFX, transform.position, miningNodeSpawnRotation, meteoroids);
                Destroy(groundImpactVFX, 5);
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
