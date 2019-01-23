using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made by Robin
/// </summary>

public class Sled : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float launchSpeed = 30;

    // For sled resetting
    private Sled sled;
    private Vector3 sledStartPosition;
    private Quaternion sledStartRotation;
    private bool coroutineRunning = false;
    private Text launchText;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        launchText = GameObject.Find("LaunchTimerText").GetComponent<Text>();
        sled = FindObjectOfType<Sled>();
        sledStartPosition = sled.transform.position;
        sledStartRotation = sled.transform.rotation;
    }

    public IEnumerator CoLaunch()
    {
        if (!coroutineRunning)
        {
            coroutineRunning = true;
            launchText.text = "3";
            yield return new WaitForSeconds(1f);
            launchText.text = "2";
            yield return new WaitForSeconds(1f);
            launchText.text = "1";
            yield return new WaitForSeconds(1f);
            launchText.text = "Launch!";
            rb.velocity = transform.TransformDirection(Vector3.up * launchSpeed);
            yield return new WaitForSeconds(9f);
            transform.position = sledStartPosition;
            transform.rotation = sledStartRotation;
            rb.velocity = Vector3.zero;
            coroutineRunning = false;
        }
    }


}
