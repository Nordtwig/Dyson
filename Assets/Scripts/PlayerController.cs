using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script creator Ulrik. Modified/cleaned by Robin, 
/// </summary>

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
    [SerializeField] private Collider interactionZone;

    public bool hasBox;
    public float playerSpeed;

    void Start() {
		rb = GetComponent<Rigidbody>();
    }

    public void PlayerMove(float moveX, float moveY)
	{
        Vector3 moveDirection = new Vector3(moveX, 0, moveY).normalized * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection));
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

        else 
        {
            interactionZone.gameObject.SetActive(true);
        }

        interactionZone.gameObject.SetActive(false);
    }
}
