using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Robin
/// </summary>

public class SafeZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteroid" || other.tag == "Node" || other.tag == "DangerZone")
        {
            Destroy(other.gameObject);
        }
    }

}
