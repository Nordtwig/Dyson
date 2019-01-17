using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private Box box;
    public bool onlyOneBox;

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Box")
        {
            //FindObjectOfType<PlayerController>().hasBox = true;
            Debug.Log("Funkar!");
            other.gameObject.GetComponent<Box>().PickUpBox();
        }
    }
}
