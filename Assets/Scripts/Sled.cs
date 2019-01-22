using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float launchSpeed = 30;

    // For sled resetting
    private Sled sled;
    private Vector3 sledStartPosition;
    private Quaternion sledStartRotation;
    private bool coroutineRunning = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        sled = FindObjectOfType<Sled>();
        sledStartPosition = sled.transform.position;
        sledStartRotation = sled.transform.rotation;
    }

    public IEnumerator Launch()
    {
        if (!coroutineRunning)
        {
            coroutineRunning = true;
            rb.velocity = transform.TransformDirection(Vector3.up * launchSpeed);
            yield return new WaitForSeconds(10);
            transform.position = sledStartPosition;
            transform.rotation = sledStartRotation;
            rb.velocity = Vector3.zero;
            coroutineRunning = false;
        }
        yield return null;
    }


}
