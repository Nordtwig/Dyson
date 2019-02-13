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

    private static string RUNNING_BOOL_STRING = "isRunning";
    private static string BACKING_BOOL_STRING = "isBacking";
    private static string STRAFE_LEFT_BOOL_STRING = "isStrafingLeft";
    private static string STRAFE_RIGHT_BOOL_STRING = "isStrafingRight";
    private static string JUMPING_BOOL_STRING = "isJumping";
    private static string HOLDING_ITEM_BOOL_STRING = "isHoldingItem";

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
    public float holdCoefficient = 1.5f;
    GameObject heldBox;
    GameObject heldRig;
    GameObject heldSanctuary;

    Animator astronautController;

    void Start() {
        interactionZone = FindObjectOfType<InteractionZone>();
        interactionZone.gameObject.SetActive(false);
		rb = GetComponent<Rigidbody>();
        RotX = GameObject.Find("RotX");
        model = this.gameObject.transform.GetChild(1).gameObject;
        basePlayerSpeed = playerSpeed;
        astronautController = transform.GetComponentInChildren<Animator>();


        heldRig = GameObject.Find("HoldingRig");
        heldBox = GameObject.Find("HoldingBox");
        heldSanctuary = GameObject.Find("HoldingSanctuary");

        heldRig.gameObject.SetActive(false);
        heldBox.gameObject.SetActive(false);
        heldSanctuary.gameObject.SetActive(false);
    }

    public void PlayerMove(float moveX, float moveY)
	{
        if (grounded)
        {
            moveDirection = new Vector3(moveX, 0, moveY).normalized * Time.deltaTime;

            UpdateAnimatorBools(moveX, moveY);

            if (moveDirection.magnitude != 0)
            {
                playerSpeed = basePlayerSpeed;
            }
            else
            {
                playerSpeed = 0;
            }

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

    private void UpdateAnimatorBools(float x, float y)
    {
        if (x > 0)
        {
            astronautController.SetBool(STRAFE_RIGHT_BOOL_STRING, true);
            astronautController.SetBool(STRAFE_LEFT_BOOL_STRING, false);
        }
        else if (x < 0)
        {
            astronautController.SetBool(STRAFE_LEFT_BOOL_STRING, true);
            astronautController.SetBool(STRAFE_RIGHT_BOOL_STRING, false);
        }
        else
        {
            astronautController.SetBool(STRAFE_LEFT_BOOL_STRING, false);
            astronautController.SetBool(STRAFE_RIGHT_BOOL_STRING, false);
        }

        if (y > 0)
        {
            astronautController.SetBool(RUNNING_BOOL_STRING, true);
            astronautController.SetBool(BACKING_BOOL_STRING, false);
        }
        else if (y < 0)
        {
            astronautController.SetBool(BACKING_BOOL_STRING, true);
            astronautController.SetBool(RUNNING_BOOL_STRING, false);
        }
        else
        {
            astronautController.SetBool(BACKING_BOOL_STRING, false);
            astronautController.SetBool(RUNNING_BOOL_STRING, false);
        }
    }

    public void PlayerJump()
    {
        if (grounded)
        {
            rb.velocity += transform.TransformDirection(Vector3.up * jumpHeight);
            playerSpeed = basePlayerSpeed * 1.5f;
            AudioManager.instance.PlayOnPos("Jump", transform);
            astronautController.SetBool(JUMPING_BOOL_STRING, true);
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
        if (holdingItem && GetComponentInChildren<PickupableObject>(true))
        {
            PickupableObject item = GetComponentInChildren<PickupableObject>(true);
            DisableHoldingItem();

            item.DropItem();
        }
    }

    public void ThrowItem(float throwStrength)
    {
        if (holdingItem)
        {
            if (GetComponentInChildren<SanctuaryItem>(true))
            {
                SanctuaryItem sanctuary = GetComponentInChildren<SanctuaryItem>(true);
                sanctuary.ThrowItem(throwStrength);
                SetEnableHoldingSanctuary(false);
            }
            else if (GetComponentInChildren<Box>(true))
            {
                Box box = GetComponentInChildren<Box>(true);
                box.ThrowItem(throwStrength);
                SetEnableHoldingBox(false);
            }
            else if (GetComponentInChildren<Chunk>(true))
            {
                Chunk chunk = GetComponentInChildren<Chunk>(true);
                chunk.ThrowItem(throwStrength);
            }
            else if (GetComponentInChildren<MiningRig>(true))
            {
                MiningRig rig = GetComponentInChildren<MiningRig>(true);
                rig.ThrowItem(throwStrength);
                SetEnableHoldingRig(false);
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
            astronautController.SetBool(JUMPING_BOOL_STRING, false);
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
        astronautController.SetBool(JUMPING_BOOL_STRING, true);
    }

    public void ContinuedJump()
    {
        rb.velocity += transform.TransformDirection(Vector3.up * Time.deltaTime * 4);
    }

    public void SetHoldingItem(bool set)
    {
        holdingItem = set;
        astronautController.SetBool(HOLDING_ITEM_BOOL_STRING, set);
    }

    public void SetEnableHoldingRig(bool set)
    {
        heldRig.SetActive(set);
        SetHoldingItem(set);
    }

    public void SetEnableHoldingSanctuary(bool set)
    {
        heldSanctuary.SetActive(set);
        SetHoldingItem(set);
    }

    public void SetEnableHoldingBox(bool set)
    {
        heldBox.SetActive(set);
        SetHoldingItem(set);
    }

    private void DisableHoldingItem()
    {
        heldBox.SetActive(false);
        heldSanctuary.SetActive(false);
        heldRig.SetActive(false);
        SetHoldingItem(false);
    }

    public void WeightUpdate()
    {
        if (pickedUpItem == true)
        {
            basePlayerSpeed = basePlayerSpeed / holdCoefficient;
            playerAirControllSpeed = playerAirControllSpeed / holdCoefficient;
            jumpHeight = jumpHeight / holdCoefficient;
        }
        else
        {
            basePlayerSpeed = basePlayerSpeed * holdCoefficient;
            playerAirControllSpeed = playerAirControllSpeed * holdCoefficient;
            jumpHeight = jumpHeight * holdCoefficient;
        }
    }
}
