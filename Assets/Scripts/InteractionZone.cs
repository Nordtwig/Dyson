using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Svedlund and Heimer, modified by Heimer
/// </summary>

public class InteractionZone : MonoBehaviour
{
    private Box box;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Box" && !player.GetComponent<PlayerController>().hasBox)
        {
            other.gameObject.GetComponent<Box>().PickUpBox();
        }
        else if (other.tag == "Rig" && !other.gameObject.GetComponent<MiningRig>().functioning && !player.GetComponent<PlayerController>().hasBox)
        {
            other.gameObject.GetComponent<MiningRig>().Repair();
        }
        else if (other.tag == "Rig" && !player.GetComponent<PlayerController>().hasBox)
        {
            other.gameObject.GetComponent<MiningRig>().PickUpRig();
        }
    }
}
