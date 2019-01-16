using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {

    GravityAttractor asteroid;

    void Awake() {
        asteroid = GameObject.FindGameObjectWithTag("Asteroid").GetComponent<GravityAttractor>();
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate() {
        asteroid.Attract(transform);
    }
}
