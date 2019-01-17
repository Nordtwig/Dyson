using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningRig : MonoBehaviour
{

    [SerializeField] bool pickedUp;
    private GameObject player;
    public GameObject box;
    public GameObject rigStatus;
    MeshRenderer rend;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rend = rigStatus.GetComponent<MeshRenderer>();
    }

    //Rig is parented to player on pickup, despawns and changes a bool to indicate the player is carrying it
    public void PickUpRig()
    {
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
        pickedUp = true;
    }

    public void ReleaseRig()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        pickedUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            rend.material.color = Color.green;
            if (!pickedUp)
            {
                rend.material.color = Color.white;
                //Code to start mining process
                BoxSpawn();
            }
        }
    }

    private void OnTriggerExit(Collider MiningNode)
    {
        rend.material.color = Color.red;
    }

    private void BoxSpawn()
    {
        Instantiate(box, new Vector3(Random.Range(transform.position.x + 1, transform.position.x + 10), transform.position.y + 1, transform.position.z), 
                    Quaternion.identity);
    }
}
