using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Svedlund and Heimer, modified by Heimer, Robin
/// </summary>

public class InteractionZone : MonoBehaviour
{
    private GameObject player;
    private AudioManager audioManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LaunchButton" && GameController.instance.GetAmountOfDeliveredBoxes() >= GameController.instance.phaseAmount)
        {
            FindObjectOfType<Sled>().StartLaunchCo();
            GameController.instance.InvokeIncrementPhase(3);
            FindObjectOfType<ProgressBarScript>().ProgressBarUpdate();
        }
        else if (other.tag == "LaunchButton" && GameController.instance.GetAmountOfDeliveredBoxes() <= GameController.instance.phaseAmount)
        {
            Debug.Log("You don't have enough of a load to send it yet");
        }
        else if (other.tag == "Box" && !player.GetComponent<PlayerController>().holdingItem)
        {
            player.GetComponent<PlayerController>().holdingItem = true;
            other.gameObject.GetComponent<Box>().PickUpBox();
            audioManager.Play("Pickup");
        }
        else if (other.tag == "Chunk" && !player.GetComponent<PlayerController>().holdingItem)
        {
            player.GetComponent<PlayerController>().holdingItem = true;
            other.gameObject.GetComponent<Chunk>().PickUpChunk();
            audioManager.Play("Pickup");
        }
        else if (other.tag == "SanctuaryHolder" && !player.GetComponent<PlayerController>().holdingItem)
        {
            player.GetComponent<PlayerController>().holdingItem = true;
            other.gameObject.GetComponent<Box>().PickUpBox();
        }
        else if (other.tag == "Rig" && !other.gameObject.GetComponent<MiningRig>().functioning && !player.GetComponent<PlayerController>().holdingItem)
        {
            other.gameObject.GetComponentInChildren<MiningRig>().Repair();
        }
        else if (other.tag == "Rig" && !player.GetComponent<PlayerController>().holdingItem)
        {
            other.gameObject.GetComponentInChildren<MiningRig>().StartCoPickUpRig();
            audioManager.Play("Pickup");
        }
    }
}
