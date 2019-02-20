using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Robin
/// </summary>
public class SafetyNet : MonoBehaviour
{
    //A simple safetyNet on a trigger inside the asteroid to make sure you don't lose your stuff if they somehow gets dropped into it.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rig" || other.tag == "Chunk" || other.tag == "Box" || other.tag == "SanctuaryHolder")
            other.transform.position = other.transform.position + other.transform.TransformDirection(Vector3.up) * 20;
    }
}
