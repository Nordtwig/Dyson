using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator Ulrik. Modified/cleaned by Robin, 
/// </summary>

public class PlayerController : MonoBehaviour {

	public Rigidbody rb;
    [SerializeField] private Collider interactionZone;

    private GameObject RotX;
    private GameObject model;

    public bool hasBox;
    public float playerSpeed;
    public float jumpHeight = 10;
    private float basePlayerSpeed;
    private bool coRunning = false;
    private bool grounded = false;
    private float rotationSpeed = 5f;
    Vector3 moveDirection = Vector3.zero;

    void Start() {
		rb = GetComponent<Rigidbody>();
        RotX = GameObject.Find("RotX");
        model = this.gameObject.transform.GetChild(1).gameObject;
        basePlayerSpeed = playerSpeed;
    }

    public void PlayerMove(float moveX, float moveY)
	{
        if (grounded)
        {
            moveDirection = new Vector3(moveX, 0, moveY).normalized * playerSpeed * Time.deltaTime;
            transform.rotation = RotX.transform.rotation;
        }
        model.transform.rotation = RotX.transform.rotation;
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection));

    }

    public void PlayerJump()
    {
        if (grounded)
        {
            grounded = false;
            rb.velocity += transform.TransformDirection(Vector3.up * jumpHeight);
            playerSpeed *= 2;
        }
    }

    public void PlayerInteraction()
    {
        if (hasBox)
        {
            hasBox = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Asteroid" || collision.collider.tag == "Ramp")
        {
            grounded = true;
            playerSpeed = basePlayerSpeed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Asteroid" || collision.collider.tag == "Ramp")
        {
            grounded = false;
            playerSpeed = basePlayerSpeed;
        }
    }
    
    public void ContinuedJump()
    {
        rb.velocity += transform.TransformDirection(Vector3.up * Time.deltaTime * 5);
    }
}
