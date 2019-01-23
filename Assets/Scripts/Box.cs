using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Svedlund and Heimer
/// </summary>

public class Box : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //box is parented to player and disabled for transport, player hasBox bool sets to true
    public void PickUpBox()
    {
        player.GetComponent<PlayerController>().hasBox = true;
        transform.SetParent(player.transform);
        gameObject.SetActive(false);
    }

    //Box is unparented, reactivated and placed in front of player. player hasBox bool is set to false
    public void DropBox()
    {
        player.GetComponent<PlayerController>().hasBox = false;
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        transform.position = player.transform.position + player.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteroid")
        {
            Destroy(gameObject);
        }
    }
}
