using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator Petter, Modified by Heimer, slightly Robin
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

    private AudioSource meteoriteLoop;


    private PlayerController player;
    private GameObject[] boxes;
    private GameObject[] miningRigs;
	private float meteoroidHitBox = 7f;
	[SerializeField] float upForce = 2;
	[SerializeField] float sideForce = 100;
    [SerializeField] int minDistanceReduction = 10;
    [SerializeField] int maxDistanceReduction = 15;

    private Transform meteoroids;
    private bool headingTowardsSanctuary;
    private Collider sanctuaryCollider;

    private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        meteoroids = GameObject.Find("Meteoroids").transform;
        miningNode = GameObject.Find("GetableMiningNode");
        dangerZone = GameObject.Find("GetableDangerZone");
        AudioSource[] audios = GetComponents<AudioSource>();
        meteoriteLoop = audios[0];

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
                Vector3 boxDistance = hitColliders[i].transform.position - transform.position;
                hitColliders[i].GetComponent<Rigidbody>().velocity = hitColliders[i].transform.TransformDirection((Vector3.up / Mathf.Clamp(boxDistance.sqrMagnitude, minDistanceReduction, maxDistanceReduction)) * upForce) + (boxDistance.normalized / Mathf.Clamp(boxDistance.sqrMagnitude, minDistanceReduction, maxDistanceReduction)) * sideForce;
            }

            if (hitColliders[i].tag == "Player")
            {
                Vector3 playerDistance = player.transform.position - transform.position;
                player.rb.velocity = player.transform.TransformDirection((Vector3.up / Mathf.Clamp(playerDistance.sqrMagnitude, minDistanceReduction, maxDistanceReduction)) * upForce) + (playerDistance.normalized / Mathf.Clamp(playerDistance.sqrMagnitude, minDistanceReduction, maxDistanceReduction)) * sideForce;
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
            StartCoroutine(CoFadeOut(meteoriteLoop, 0.2f));
            Destroy(zone);
            Invoke("DestroyMeteroid", 0.5f);
        }
    }

    public static IEnumerator CoFadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
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
