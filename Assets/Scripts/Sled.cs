using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float launchSpeed = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch()
    {
        rb.velocity = transform.TransformDirection(Vector3.up * launchSpeed);
    }


}
