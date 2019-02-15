using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
    [SerializeField] private bool rotationFreeze = true;

	[SerializeField, Tooltip("1 = Unmodified, 1.X+ = Falls faster, 0.X = Falls slower")]
	float fallingSpeed = 1f;
    GravityAttractor asteroid;

    void Awake() {
        asteroid = GameObject.FindGameObjectWithTag("Asteroid").GetComponent<GravityAttractor>();
        GetComponent<Rigidbody>().useGravity = false;
        if (rotationFreeze)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate() {
        asteroid.Attract(transform, fallingSpeed);
    }
}
