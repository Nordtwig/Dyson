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

    Animator astronautController;

    void Start() {
        interactionZone = FindObjectOfType<InteractionZone>();
        interactionZone.gameObject.SetActive(false);
		rb = GetComponent<Rigidbody>();
        RotX = GameObject.Find("RotX");
        model = this.gameObject.transform.GetChild(1).gameObject;
        basePlayerSpeed = playerSpeed;
        astronautController = transform.GetComponentInChildren<Animator>();

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
            if (moveX == 0 && moveY == 0)
            {
                astronautController.SetBool("isRunning", false); 
                astronautController.SetBool("isBacking", false); 
            }
            else if (moveY > 0)
            {
                astronautController.SetBool("isRunning", true);
                astronautController.SetBool("isBacking", false);
            }
            else if (moveY < 0)
            {
                astronautController.SetBool("isBacking", true);
                astronautController.SetBool("isRunning", false);
            }
            else
            {
                astronautController.SetBool("isRunning", true);
                astronautController.SetBool("isBacking", false);
            }
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
            astronautController.SetBool("isJumping", true);
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
            DropItem();
        pickedUpItem = false;
        hitButton = false;
        coRunning = false;
        yield return null;
    }

    private void DropItem()
    {
        if (holdingItem)
        {
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
            else
            {
                return;
            }

            SetHoldingItem(false);
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
            else
            {
                return;
            }

            SetHoldingItem(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Sanctuary" && other.tag != "DangerZone")
        {
            grounded = true;
            astronautController.SetBool("isJumping", false);
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
        astronautController.SetBool("isJumping", true);
    }

    public void ContinuedJump()
    {
        rb.velocity += transform.TransformDirection(Vector3.up * Time.deltaTime * 4);
    }

    public void SetHoldingItem(bool set)
    {
        holdingItem = set;
        astronautController.SetBool("isHoldingItem", set);
    }
}
