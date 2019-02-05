using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Svedlund and Heimer, slightly midified by Robin
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
        if (tag == "SanctuaryHolder")
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
            for(int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "Rig")
                {
                    hitColliders[i].transform.GetComponent<MiningRig>().shielded = false;
                }
            }
        }
        gameObject.SetActive(false);
    }

    //Box is unparented, reactivated and placed in front of player. player hasBox bool is set to false
    public void DropBox()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        transform.position = player.transform.position + player.model.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 4);
        rb.velocity = Vector3.zero;
    }

    public void ThrowBox(float throwStrength)
    {
        DropBox();
        rb.velocity = player.transform.TransformDirection(Vector3.forward * player.playerSpeed) + player.model.transform.TransformDirection(Vector3.up * 2 + Vector3.forward * 5 * throwStrength);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            AudioManager.instance.PlayOnPos("Box Collision", transform);
        }
    }
}
