using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Svedlund and Heimer
/// </summary>

public class Box : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody rb;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    //box is parented to player and disabled for transport, player hasBox bool sets to true
    public void PickUpBox()
    {
        transform.SetParent(player.transform);
        gameObject.SetActive(false);
    }

    //Box is unparented, reactivated and placed in front of player. player hasBox bool is set to false
    public void DropBox()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        transform.position = player.transform.position + player.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 3);
    }

    public void ThrowBox()
    {
        DropBox();
        rb.velocity += player.rb.velocity * 2 + player.transform.TransformDirection(Vector3.up * 5 + Vector3.forward * 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteroid")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            AudioManager.instance.PlayOnPos("Box Collision", transform.position);
        }
    }
}
