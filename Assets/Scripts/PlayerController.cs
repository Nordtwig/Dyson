using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;


    public float playerSpeed;

    void Start() {
		rb = GetComponent<Rigidbody>();
    }

    void Update() {
		PlayerMove();
    }

	public void PlayerMove()
	{
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection));
    }

}
