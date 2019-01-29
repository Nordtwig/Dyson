using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// modified by Heimer, Christoffer Brandt, Robin
/// </summary>

public class MiningRig : MonoBehaviour
{
    [SerializeField] private bool pickedUp;
    [SerializeField] private int timeBetweenBoxes = 2;
    private GameObject box;
    private PlayerController player;
    private MeshRenderer rigStatusRend;
    private Animator animator;
    private MiningNode minedNode; //currently minedNode set to null when no minedNode
    private Color baseColor;
    private Rigidbody rb;
    private bool coBoxSpawnRunning = false;

    public bool functioning = true;

    void Start()
    {
        box = GameObject.Find("GetableBox");
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        rigStatusRend = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        rigStatusRend.material.color = Color.red;
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (rigStatusRend != null)
        {
            rigStatusRend.material.color = Color.red;
        }
    }

    //Rig is parented to player on pickup, despawns and changes a bool to indicate the player is carrying it
    private IEnumerator CoPickUpRig()
    {
        player.holdingItem = true;

        if (animator.GetBool("OnNodeDeploy") || animator.GetBool("Empty") || animator.GetBool("OnMining"))
        {
            animator.SetBool("Empty", false);
            animator.SetBool("OnMining", false);
            animator.SetBool("OnNodeDeploy", false);
            animator.SetBool("OnPickUp", true);
            pickedUp = true;
            //yield return new WaitForSeconds(2f);
        }
        else
        {
            animator.SetBool("Empty", false);
            animator.SetBool("OnMining", false);
            animator.SetBool("OnNodeDeploy", false);
            animator.SetBool("OnPickUp", true);
            pickedUp = true;
        }
        coBoxSpawnRunning = false;
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
        yield return null;
    }

    public void StartCoPickUpRig()
    {
        StartCoroutine("CoPickUpRig");
    }

    //Rig is un-parented, spawns in front of player and changes the bool to false
    public void DropRig()
    {
        animator.SetBool("OnPickUp", false);
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        pickedUp = false;
        transform.position = player.transform.position + player.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 3);
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	}

    public void ThrowRig(float throwStrength)
    {
        DropRig();
        rb.velocity += player.rb.velocity*2 + player.transform.TransformDirection(Vector3.up * 5 + Vector3.forward * 5 * throwStrength);
    }

    //If the object collides with the "Node" tag AND picked up is false(released), changes color to green and starts spawning boxes
    private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Node")
        {
            if (!pickedUp)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                animator.SetBool("OnPickUp", false);
                animator.SetBool("OnNodeDeploy", true);
                minedNode = other.GetComponent<MiningNode>();
				transform.position = minedNode.transform.position;
                if (functioning)
                {
                    StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            AudioManager.instance.PlayOnPos("Rig Collision", transform);
        }
    }

    public void BreakRig()
    {
        functioning = false;
        rigStatusRend.material.color = Color.red;
        coBoxSpawnRunning = false;
    }

    //If exiting node collider, change color to red
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            animator.ResetTrigger("OnNodeDeploy");
            animator.SetTrigger("OnPickUp");
            minedNode = null;
            rigStatusRend.material.color = Color.red;
        }
    }

    //Coroutine that uses for loop to create boxes in the rigs proximity within a set interval
    private IEnumerator CoBoxSpawn(int resourseAmount)
    {
        if (!coBoxSpawnRunning)
        {
            coBoxSpawnRunning = true;
            yield return new WaitForSeconds(2.8f);
            animator.SetBool("OnMining", true);
            for (int i = 0; i < resourseAmount; i++)
            {
                for (int w = 0; w < timeBetweenBoxes; w++)
                {
                    if (functioning && minedNode)
                    {
                        rigStatusRend.material.color = Color.green;
                    }
                    else
                    {
                        rigStatusRend.material.color = Color.red;
                    }
                    yield return new WaitForSeconds(1);
                }

                if (functioning && minedNode)
                {
                    rigStatusRend.material.color = Color.green;
                    EjectBox();
                    if (!minedNode.OnBoxSpawn()) //Do when empty 
                    {
                        minedNode = null;
                        rigStatusRend.material.color = Color.red;
                    }
                }
                else
                {
                    rigStatusRend.material.color = Color.red;
                }

                if (pickedUp)
                {
                    yield break;
                }
            }
            rigStatusRend.material.color = Color.red;
            animator.SetBool("OnMining", false);
            animator.SetBool("OnNodeDeploy", false);
            animator.SetBool("Empty", true);
            coBoxSpawnRunning = false;
        }
        yield return null;
    }

    public void Repair()
    {
        functioning = true;
        if (minedNode)
        {
            StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
        }
    }

    //Ejects a boc in a random direction from the MiningRig
    public void EjectBox()
    {
        GameObject go = Instantiate(box, transform.position + transform.TransformDirection(Vector3.up * 3), Quaternion.identity);
        go.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * 15 + Vector3.forward * Random.Range(-10, 10) + Vector3.right * Random.Range(-10, 10));
    }
}
