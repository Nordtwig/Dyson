using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rig" || other.tag == "Chunk" || other.tag == "Box" || other.tag == "SanctuaryHolder")
            other.transform.position = other.transform.position + other.transform.TransformDirection(Vector3.up) * 20;
    }
}
