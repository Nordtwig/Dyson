using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Noah Nordqvist
/// </summary>

public class GravityAttractor : MonoBehaviour {

    public float gravity = -10f;

	public void Attract(Transform body, float fallSpeed) {
        Vector3 targetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetDir * gravity * fallSpeed);
    }
}
