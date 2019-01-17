using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningRig : MonoBehaviour
{

    [SerializeField] bool pickedUp;
    [SerializeField] int timeBetweenBoxes = 2;
    private GameObject player;
    public GameObject box;
    public GameObject rigStatus;
    MeshRenderer rend;
    
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
        pickedUp = true;
    }

    //Rig is un-parented, spawns on players position and changes the bool to false
    public void ReleaseRig()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        pickedUp = false;
    }

    //If the object collides with the "Node" tag AND picked up is false(released), changes color to green and starts spawning boxes
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            if (!pickedUp)
            {
                rend.material.color = Color.green;
                StartCoroutine(CoBoxSpawn());
            }
        }
    }

    //If exiting node collider, change color to red
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            rend.material.color = Color.red;
        }
    }

    //Coroutine that uses for loop to create boxes in the rigs proximity within a set interval
    private IEnumerator CoBoxSpawn()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(box, new Vector3(Random.Range(player.transform.position.x + 1, player.transform.position.x + 10), player.transform.position.y + 10,
                        player.transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenBoxes);
            if (pickedUp)
            {
                yield break;
            }
        }
    }
}
