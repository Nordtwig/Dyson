using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator Ulrik. Modified/cleaned by Robin, 
/// </summary>

public class PlayerController : MonoBehaviour {

	public Rigidbody rb;
    private InteractionZone interactionZone;

    private GameObject RotX;
    private GameObject model;

    public bool holdingItem;
    public float playerSpeed;
    public float jumpHeight = 10;
    private float basePlayerSpeed;
    private bool coRunning = false;
    private bool grounded = false;
    private float rotationSpeed = 5f;
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
            moveDirection = transform.TransformDirection(new Vector3(moveX, 0, moveY).normalized * Time.deltaTime);
            transform.rotation = RotX.transform.rotation;
            model.transform.rotation = RotX.transform.rotation;
            rb.MovePosition(rb.position + moveDirection * playerSpeed);
            timeOfJump = Time.time;
        }
        else
        {
            airMoveDirection = new Vector3(moveX, 0, moveY).normalized * Time.deltaTime;
            model.transform.rotation = RotX.transform.rotation;
            rb.MovePosition(rb.position + transform.TransformDirection(moveDirection * playerSpeed));
            if (airMoveDirection != Vector3.zero)
            {
                rb.velocity += transform.TransformDirection(airMoveDirection * playerSpeed);

            }
        }
        

    }

    public void PlayerJump()
    {
        if (grounded)
        {
            rb.velocity += transform.TransformDirection(Vector3.up * jumpHeight);
            playerSpeed *= 2;
        }
    }

    public void PlayerInteraction()
    {
        if (holdingItem)
        {
            holdingItem = false;
            if (GetComponentInChildren<Box>(true))
            {
                Box box = GetComponentInChildren<Box>(true);
                box.DropBox();
            }
            else if (GetComponentInChildren<MiningRig>(true))
            {
                MiningRig rig = GetComponentInChildren<MiningRig>(true);
                rig.DropRig();
            }
        }

        else if (!coRunning)
        {
            StartCoroutine(CoInteractionZoneHandler());
        }
    }

	private IEnumerator CoInteractionZoneHandler()
    {
        interactionZone.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        interactionZone.gameObject.SetActive(false);
        yield return null;
    }

    public void ThrowItem()
    {
        if (holdingItem)
        {
            if (GetComponentInChildren<Box>(true))
            {
                Box box = GetComponentInChildren<Box>(true);
                box.ThrowBox();
            }
            else if (GetComponentInChildren<MiningRig>(true))
            {
                MiningRig rig = GetComponentInChildren<MiningRig>(true);
                rig.ThrowRig();
            }
            holdingItem = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Sanctuary")
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Sanctuary")
        {
            playerSpeed = basePlayerSpeed;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grounded = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Asteroid")
    //    {
    //        grounded = true;
    //        playerSpeed = basePlayerSpeed;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.tag == "Asteroid")
    //    {
    //        grounded = false;
    //    }
    //}

    public void ContinuedJump()
    {
        rb.velocity += transform.TransformDirection(Vector3.up * Time.deltaTime * 5);
    }
}
