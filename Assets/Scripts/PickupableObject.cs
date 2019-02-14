using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Robin
/// </summary>

public abstract class PickupableObject : MonoBehaviour
{
    protected PlayerController player;
    public Rigidbody rb;
    protected AudioManager audioManager;

    protected virtual void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    //Item is parented to player and disabled for transport
    public virtual void PickUpItem()
    {
        audioManager.Play("Pickup");
        transform.SetParent(player.transform);
        gameObject.SetActive(false);
    }

    //Item is unparented, reactivated and placed in front of player.
    public virtual void DropItem()
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        transform.position = player.transform.position + player.model.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 2);
        rb.velocity = Vector3.zero;
    }

    public virtual void ThrowItem(float throwStrength)
    {
        DropItem();
        rb.velocity = player.model.transform.TransformDirection(Vector3.forward * player.playerSpeed) + player.model.transform.TransformDirection(Vector3.up * 2 + Vector3.forward * 10 * throwStrength);
    }

    protected abstract void OnCollisionEnter(Collision collision);
    
}
