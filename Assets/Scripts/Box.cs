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
        Debug.Log("A box has been picked up");
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
        player.GetComponent<PlayerController>().hasBox = true;
    }

    public void DropBox()
    {
        Debug.Log("A box has been dropped");
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        player.GetComponent<PlayerController>().hasBox = false;
        transform.position = player.transform.position + player.transform.TransformDirection(Vector3.forward * 3);
    }
}
