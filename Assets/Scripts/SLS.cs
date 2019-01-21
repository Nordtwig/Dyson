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
       
        if (other.tag == "Box")
        {
            //Debug.Log("Box has been delivered");
            Destroy(other.gameObject);
            GameController.instance.BoxDelivered();
        }
    }
}
