using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Heimer
/// </summary>

public class SLS : MonoBehaviour
{
    private AudioSource getBox;

    private void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        getBox = audios[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box" && GameController.instance.GetAmountOfDeliveredBoxes() < GameController.instance.phaseBoxAmount)
        {
            Destroy(other.gameObject);
            GameController.instance.BoxDelivered();
            getBox.Play();
        }
        else if (other.tag == "Box")
        {
            other.transform.GetComponent<Rigidbody>().velocity += other.transform.TransformDirection(Vector3.up * 20 + Vector3.forward * Random.Range(-5, 5) + Vector3.right * Random.Range(-5, 5));
        }
    }
}
