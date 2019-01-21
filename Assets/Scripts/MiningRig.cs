using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool functioning = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rend = rigStatus.GetComponent<MeshRenderer>();
        rend.material.color = Color.red;
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

    //Rig is un-parented, spawns on players position and changes the bool to false
    public void DropRig()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        pickedUp = false;
        player.GetComponent<PlayerController>().hasBox = false;
        transform.position = player.transform.position + player.transform.TransformDirection(Vector3.forward * 3);
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
                    rend.material.color = Color.green;
                    StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
                }
            }
        }

        if (other.tag == "Meteroid")
        {
            functioning = false;
            casing.GetComponent<MeshRenderer>().material.color = new Color(0.6f, 0, 0);
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
                if (!minedNode.OnBoxSpawn()) //Do when empty 
                {
                    minedNode = null;
                    rend.material.color = Color.red;
                }
                Instantiate(box, new Vector3(Random.Range(player.transform.position.x + 1, player.transform.position.x + 10), player.transform.position.y + 10,
                        player.transform.position.z), Quaternion.identity);
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
        minedNode = null;
        rend.material.color = Color.red;
        yield return null;
    }

    public void Repair()
    {
        functioning = true;
        casing.GetComponent<MeshRenderer>().material.color = Color.white;
        if (minedNode)
        {
            StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
        }
    }
}
