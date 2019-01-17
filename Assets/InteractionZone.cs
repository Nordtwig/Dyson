using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private Box box;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Box")
        {
            Debug.Log("Funkar!");
            other.gameObject.GetComponent<Box>().PickUpBox();
        }
    }
}
