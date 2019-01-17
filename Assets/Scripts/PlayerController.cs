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

	void PlayerMove()
	{
		//playerMoveX = Input.GetAxisRaw("Horizontal");
		//playerMoveY = Input.GetAxisRaw("Vertical");

		Vector3 playerMoveY = Vector3.forward * Input.GetAxisRaw("Vertical");
		Vector3 playerMoveX = Vector3.right * Input.GetAxisRaw("Horizontal");

		Vector3 movement = playerMoveY + playerMoveX;
		movement = movement.normalized * playerSpeed;

		rb.velocity = movement;
	}
}
