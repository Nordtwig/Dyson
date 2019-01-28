using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanctuary : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DangerZone")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Meteroid" || other.tag == "Node")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
