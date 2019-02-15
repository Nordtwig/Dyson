using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made by Robin, slightly modified by Heimer (adding sfx)
/// </summary>

public class Sled : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float launchSpeed = 30;

    private AudioSource sledPreLaunch;
    private AudioSource sledLaunch;


    // For sled resetting
    private Sled sled;
    private Vector3 sledStartPosition;
    private Quaternion sledStartRotation;
    public bool coroutineRunning = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sled = FindObjectOfType<Sled>();
        sledStartPosition = sled.transform.position;
        sledStartRotation = sled.transform.rotation;
        AudioSource[] audios = GetComponents<AudioSource>();
        sledPreLaunch = audios[0];
        sledLaunch = audios[1];

	}

	public void StartLaunchCo()
    {
        StartCoroutine(CoLaunch());
    }

    public IEnumerator CoLaunch()
    {
        if (!coroutineRunning)
        {
            GameController.instance.hijackedTimerText = true;
            coroutineRunning = true;
            sledPreLaunch.Play();
            GameController.instance.timeText.color = Color.red;
            GameController.instance.timeText.text = "Launch in: 3";
            yield return new WaitForSeconds(1f);
            GameController.instance.timeText.text = "Launch in: 2";
            yield return new WaitForSeconds(1f);
            GameController.instance.timeText.text = "Launch in: 1";
            yield return new WaitForSeconds(1f);
            sledLaunch.Play();
            GameController.instance.timeText.text = "Launch!";
            rb.useGravity = false;
            rb.velocity = transform.TransformDirection(Vector3.forward * launchSpeed);
            yield return new WaitForSeconds(1f);
            GameController.instance.hijackedTimerText = false;
            yield return new WaitForSeconds(9f);
            rb.useGravity = true;
            transform.position = sledStartPosition;
            transform.rotation = sledStartRotation;
            rb.velocity = Vector3.zero;
            coroutineRunning = false;
        }
    }


}
