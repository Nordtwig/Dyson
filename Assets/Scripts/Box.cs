using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PickUpBox()
    {
        player.GetComponent<PlayerController>().hasBox = true;
        Debug.Log("A box has been picked up");
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
    }

    public void DropBox()
    {
        player.GetComponent<PlayerController>().hasBox = false;
        Debug.Log("A box has been dropped");
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        transform.position = player.transform.position + player.transform.TransformDirection(Vector3.forward * 3);
    }
}
