using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
    public Collider interactionZone;
    public bool hasBox;


    public float playerSpeed;

    void Start() {
		rb = GetComponent<Rigidbody>();
    }

    void Update() {
		PlayerMove();
        PlayerInteraction();
    }

    public void PlayerMove()
	{
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection));
    }

    public void PlayerInteraction()
    {
        if (Input.GetButtonDown("Jump") && hasBox)
        {
            Box box = GetComponentInChildren<Box>(true);
            box.DropBox();
        }

        else if (Input.GetButtonDown("Jump") && !hasBox)
        {
            interactionZone.gameObject.SetActive(true);
        }

        if (Input.GetButtonUp("Jump"))
        {
            interactionZone.gameObject.SetActive(false);
        }
    }
}
