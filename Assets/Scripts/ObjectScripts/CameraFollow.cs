using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Noah and Robin
/// </summary>

public class CameraFollow : MonoBehaviour
{

    private Transform anchorPos;
    private Transform asteroid;
    private Transform rotX;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    Vector3 offset;

    void Start()
    {
        anchorPos = GameObject.Find("CameraAnchor").transform;
        rotX = GameObject.Find("RotX").transform;
        asteroid = GameObject.FindGameObjectWithTag("Asteroid").transform;

        offset = anchorPos.position - gameObject.transform.position;
    }

    void Update()
    {
        gameObject.transform.position = anchorPos.position + offset;

        Vector3 v3 = transform.position - asteroid.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, v3) * transform.rotation;

        if (GameController.instance.state == GameController.GameControllerState.GAME) yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        //rotY.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f);
        rotX.localEulerAngles = new Vector3(0.0f, yaw, 0.0f);
    }
}
