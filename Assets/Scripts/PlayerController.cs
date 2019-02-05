using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator Ulrik. Modified/cleaned by Robin, 
/// </summary>

public class PlayerController : MonoBehaviour {

	[HideInInspector] public Rigidbody rb;
    private InteractionZone interactionZone;

    private GameObject RotX;
    [HideInInspector] public GameObject model;

    [HideInInspector] public bool holdingItem;
    [HideInInspector] public bool hitButton = false;
    [HideInInspector] public bool pickedUpItem = false;
    public float playerSpeed;
    public float playerAirControllSpeed = 4;
    public float jumpHeight = 7;
    public float basePlayerSpeed;
    private bool coRunning = false;
    public bool grounded = false;
    Vector3 moveDirection = Vector3.zero;
    Vector3 airMoveDirection = Vector3.zero;
    private float timeOfJump = 0;

    void Start() {
        interactionZone = FindObjectOfType<InteractionZone>();
        interactionZone.gameObject.SetActive(false);
		rb = GetComponent<Rigidbody>();
        RotX = GameObject.Find("RotX");
        model = this.gameObject.transform.GetChild(1).gameObject;
        basePlayerSpeed = playerSpeed;
    }

    public void PlayerMove(float moveX, float moveY)
	{
        if (grounded)
        {
            moveDirection = new Vector3(moveX, 0, moveY).normalized * Time.deltaTime;
            transform.rotation = RotX.transform.rotation;
            model.transform.rotation = RotX.transform.rotation;
            rb.MovePosition(rb.position + transform.TransformDirection(moveDirection * playerSpeed));
            timeOfJump = Time.time;
        }
        else
        {
            airMoveDirection = new Vector3(moveX, 0, moveY).normalized * Time.deltaTime;
            model.transform.rotation = RotX.transform.rotation;
            rb.MovePosition(rb.position + transform.TransformDirection(moveDirection * playerSpeed) + model.transform.TransformDirection(airMoveDirection * playerAirControllSpeed));
        }

    }

    public void PlayerJump()
    {
        if (grounded)
        {
            rb.velocity += transform.TransformDirection(Vector3.up * jumpHeight);
            playerSpeed = basePlayerSpeed * 1.5f;
            AudioManager.instance.PlayOnPos("Jump", transform);
        }
    }

    public void PlayerInteraction()
    {
        if (!coRunning)
        {
            StartCoroutine(CoInteractionZoneHandler());
        }
    }

	private IEnumerator CoInteractionZoneHandler()
    {
        coRunning = true;
        interactionZone.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        interactionZone.gameObject.SetActive(false);
        if (!hitButton && !pickedUpItem)
            HandleHoldingItem();
        pickedUpItem = false;
        hitButton = false;
        coRunning = false;
        yield return null;
    }

    private void HandleHoldingItem()
    {
        if (holdingItem)
        {
            holdingItem = false;
            if (GetComponentInChildren<Box>(true))
            {
                Box box = GetComponentInChildren<Box>(true);
                box.DropBox();
            }
            else if (GetComponentInChildren<Chunk>(true))
            {
                Chunk chunk = GetComponentInChildren<Chunk>(true);
                chunk.DropChunk();
            }
            else if (GetComponentInChildren<MiningRig>(true))
            {
                MiningRig rig = GetComponentInChildren<MiningRig>(true);
                rig.DropRig();
            }
        }
    }

    public void ThrowItem(float throwStrength)
    {
        if (holdingItem)
        {
            if (GetComponentInChildren<Box>(true))
            {
                Box box = GetComponentInChildren<Box>(true);
                box.ThrowBox(throwStrength);
            }
            else if (GetComponentInChildren<Chunk>(true))
            {
                Chunk chunk = GetComponentInChildren<Chunk>(true);
                chunk.ThrowChunk(throwStrength);
            }
            else if (GetComponentInChildren<MiningRig>(true))
            {
                MiningRig rig = GetComponentInChildren<MiningRig>(true);
                rig.ThrowRig(throwStrength);
            }
            holdingItem = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Sanctuary" && other.tag != "DangerZone")
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Sanctuary" && other.tag != "DangerZone")
        {
            playerSpeed = basePlayerSpeed;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grounded = false;
    }

    public void ContinuedJump()
    {
        rb.velocity += transform.TransformDirection(Vector3.up * Time.deltaTime * 4);
    }
}
