using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Heimer
/// </summary>

public class SLS : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box" && GameController.instance.GetAmountOfDeliveredBoxes() < GameController.instance.phaseAmount)
        {
            Destroy(other.gameObject);
            GameController.instance.BoxDelivered();
        }
        else if (other.tag == "Box")
        {
            other.transform.GetComponent<Rigidbody>().velocity += other.transform.TransformDirection(Vector3.up * 20 + Vector3.forward * Random.Range(-5, 5) + Vector3.right * Random.Range(-5, 5));
        }
    }
}
