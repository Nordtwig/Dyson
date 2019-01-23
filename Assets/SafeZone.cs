using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteroid" || other.tag == "Node")
        {
            Destroy(other.gameObject);
        }
    }

}
