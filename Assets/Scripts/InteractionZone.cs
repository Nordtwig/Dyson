using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Svedlund and Heimer, modified by Heimer, Robin
/// </summary>

public class InteractionZone : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LaunchButton" && GameController.instance.GetAmountOfDeliveredBoxes() >= GameController.instance.phaseAmount)
        {
            FindObjectOfType<Sled>().StartLaunchCo();
            GameController.instance.IncrementPhase();
            FindObjectOfType<ProgressBarScript>().ProgressBarUpdate();
        }
        else if (other.tag == "Box" && !player.GetComponent<PlayerController>().hasBox)
        {
            other.gameObject.GetComponent<Box>().PickUpBox();
        }
        else if (other.tag == "Rig" && !other.gameObject.GetComponent<MiningRig>().functioning && !player.GetComponent<PlayerController>().hasBox)
        {
            other.gameObject.GetComponentInChildren<MiningRig>().Repair();
        }
        else if (other.tag == "Rig" && !player.GetComponent<PlayerController>().hasBox)
        {
            other.gameObject.GetComponentInChildren<MiningRig>().PickUpRig();
        }
    }
}
