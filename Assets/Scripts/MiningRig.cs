using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// modified by Heimer, Christoffer Brandt, Robin
/// </summary>

public class MiningRig : MonoBehaviour
{
    [SerializeField] bool pickedUp;
    [SerializeField] int timeBetweenBoxes = 2;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject rigStatus;
    [SerializeField] private GameObject casing;
    private GameObject player;
    private MeshRenderer rend;
    private Animator animator;
    private MiningNode minedNode; //currently minedNode set to null when no minedNode
    private Color baseColor;

    public bool functioning = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rend = rigStatus.GetComponent<MeshRenderer>();
        rend.material.color = Color.red;
        //baseColor = casing.GetComponent<MeshRenderer>().material.color;
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (rend != null)
        {
            rend.material.color = Color.red;
        }
    }

    //Rig is parented to player on pickup, despawns and changes a bool to indicate the player is carrying it
    private IEnumerator CoPickUpRig()
    {
        
        if (animator.GetBool("OnNodeDeploy") || animator.GetBool("Empty") || animator.GetBool("OnMining"))
        {
            animator.SetBool("Empty", false);
            animator.SetBool("OnMining", false);
            animator.SetBool("OnNodeDeploy", false);
            animator.SetBool("OnPickUp", true);
            pickedUp = true;
            yield return new WaitForSeconds(2f);
        }
        else
        {
            animator.SetBool("Empty", false);
            animator.SetBool("OnMining", false);
            animator.SetBool("OnNodeDeploy", false);
            animator.SetBool("OnPickUp", true);
            pickedUp = true;
        }
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
        player.GetComponent<PlayerController>().hasBox = true;
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
        player.GetComponent<PlayerController>().hasBox = false;
        transform.position = player.transform.position + player.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 3);
    }

    //If the object collides with the "Node" tag AND picked up is false(released), changes color to green and starts spawning boxes
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            if (!pickedUp)
            {
                animator.SetBool("OnPickUp", false);
                animator.SetBool("OnNodeDeploy", true);
                minedNode = other.GetComponent<MiningNode>();
                Debug.Log(minedNode);
                if (functioning)
                {
                    StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
                }
            }
        }
    }

    public void BreakRig()
    {
        functioning = false;
        rend.material.color = Color.red;
    }

    //If exiting node collider, change color to red
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            animator.ResetTrigger("OnNodeDeploy");
            animator.SetTrigger("OnPickUp");
            minedNode = null;
            rend.material.color = Color.red;
        }
    }

    //Coroutine that uses for loop to create boxes in the rigs proximity within a set interval
    private IEnumerator CoBoxSpawn(int resourseAmount)
    {
        yield return new WaitForSeconds(2.8f);
        animator.SetBool("OnMining", true);
        for (int i = 0; i < resourseAmount; i++)
        {
            if (functioning && minedNode)
            {
                rend.material.color = Color.green;
                EjectBox();
                if (!minedNode.OnBoxSpawn()) //Do when empty 
                {
                    minedNode = null;
                    rend.material.color = Color.red;
                }
            }
            else
            {
                rend.material.color = Color.red;
            }

            yield return new WaitForSeconds(timeBetweenBoxes);
            if (pickedUp)
            {
                yield break;
            }
        }
        rend.material.color = Color.red;
        animator.SetBool("OnMining", false);
        animator.SetBool("OnNodeDeploy", false);
        animator.SetBool("Empty", true);
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
        GameObject go = Instantiate(box, transform.TransformDirection(Vector3.up * 50), Quaternion.identity);
        go.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * 15 + Vector3.forward * Random.Range(-10, 10) + Vector3.right * Random.Range(-10, 10));
    }
}
