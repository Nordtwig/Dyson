using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private Box box;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Box" && !player.GetComponent<PlayerController>().hasBox )
        {
            //FindObjectOfType<PlayerController>().hasBox = true;
            Debug.Log("Funkar!");
            other.gameObject.GetComponent<Box>().PickUpBox();
        }
    }
}
