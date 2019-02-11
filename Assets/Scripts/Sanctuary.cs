using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanctuary : MonoBehaviour
{
    private object metroid;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "DangerZone")
        {
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Meteroid")
        {
            collision.GetComponent<Meteroid>().ImpactExplosion();
            Destroy(collision.transform.parent.gameObject);
        }
        else if (collision.tag == "Node")
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
