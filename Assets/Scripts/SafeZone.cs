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
        if (other.tag == "DangerZone")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Meteroid")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

}
