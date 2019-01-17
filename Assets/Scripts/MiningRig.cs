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
        rend.material.color = Color.red;
    }

    private void OnEnable()
    {
        rend.material.color = Color.red;
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
        Debug.Log("trigging");
        if (other.tag == "Node")
        {
            Debug.Log("inNode");
            if (!pickedUp)
            {
                rend.material.color = Color.green;
                //Code to start mining process
                Invoke("BoxSpawn", 2f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            rend.material.color = Color.red;
        }
    }

    private void BoxSpawn()
    {
        Instantiate(box, new Vector3(Random.Range(player.transform.position.x + 1, player.transform.position.x + 10), player.transform.position.y + 10,
                    player.transform.position.z), Quaternion.identity);
    }
}
