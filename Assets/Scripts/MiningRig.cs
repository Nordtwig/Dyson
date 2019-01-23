using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// modified by Heimer
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
    private MiningNode minedNode; //currently minedNode set to null when no minedNode
    private Color baseColor;

    public bool functioning = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rend = rigStatus.GetComponent<MeshRenderer>();
        rend.material.color = Color.red;
        baseColor = casing.GetComponent<MeshRenderer>().material.color;
    }

    private void OnEnable()
    {
        if (rend != null)
        {
            rend.material.color = Color.red;
        }
    }

    //Rig is parented to player on pickup, despawns and changes a bool to indicate the player is carrying it
    public void PickUpRig()
    {

        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
        player.GetComponent<PlayerController>().hasBox = true;
        pickedUp = true;
    }

    //Rig is un-parented, spawns in front of player and changes the bool to false
    public void DropRig()
    {
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
                minedNode = other.GetComponent<MiningNode>();
                Debug.Log(minedNode);
                if (functioning)
                {
                    StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
                }
            }
        }

        if (other.tag == "Meteroid")
        {
            functioning = false;
            casing.GetComponent<MeshRenderer>().material.color = new Color(0.6f, 0, 0);
            rend.material.color = Color.red;
        }
    }

    //If exiting node collider, change color to red
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            minedNode = null;
            rend.material.color = Color.red;
        }
    }

    //Coroutine that uses for loop to create boxes in the rigs proximity within a set interval
    private IEnumerator CoBoxSpawn(int resourseAmount)
    {
        for (int i = 0; i < resourseAmount; i++)
        {
            if (functioning && minedNode)
            {
                rend.material.color = Color.green;
                Instantiate(box, transform.TransformDirection(Vector3.up * 55 + Vector3.right * Random.Range(-10, 10) + Vector3.forward * Random.Range(-10, 10)), Quaternion.identity);
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
        yield return null;
    }

    public void Repair()
    {
        functioning = true;
        casing.GetComponent<MeshRenderer>().material.color = baseColor;
        if (minedNode)
        {
            StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
        }
    }
}
