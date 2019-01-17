using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private GameObject player;

    public void PickUpBox()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("A box has been picked up");
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        gameObject.SetActive(false);
    }
    public void ScoreBox()
    {
        Debug.Log("A box has been scored");
        Destroy(gameObject);
    }

    public void DropBox()
    {
        Debug.Log("A box has been dropped");
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
    }
}
