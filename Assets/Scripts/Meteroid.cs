using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteroid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
