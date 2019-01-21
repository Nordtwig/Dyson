using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
    [SerializeField] private bool rotationFreeze = true;
    GravityAttractor asteroid;

    void Awake() {
        asteroid = GameObject.FindGameObjectWithTag("Asteroid").GetComponent<GravityAttractor>();
        GetComponent<Rigidbody>().useGravity = false;
        if (rotationFreeze)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate() {
        asteroid.Attract(transform);
    }
}
