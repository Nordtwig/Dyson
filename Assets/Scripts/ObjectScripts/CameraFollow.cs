﻿using System.Collections;
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

    private float yaw = 0.0f;

    Vector3 offset;

    void Start()
    {
        anchorPos = GameObject.Find("CameraAnchor").transform;
        rotX = GameObject.Find("RotX").transform;
        asteroid = GameObject.FindGameObjectWithTag("Asteroid").transform;
        gameObject.transform.position = anchorPos.position;
        offset = anchorPos.position - gameObject.transform.position;
    }

    void Update()
    {
        gameObject.transform.position = anchorPos.position + offset;

        Vector3 v3 = transform.position - asteroid.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, v3) * transform.rotation;

        if (GameController.instance.state == GameController.GameControllerState.GAME)
            yaw = (yaw + speedH * Input.GetAxis("Mouse X"));
        //pitch -= speedV * Input.GetAxis("Mouse Y");

        rotX.localRotation = Quaternion.Lerp(rotX.localRotation, Quaternion.Euler(new Vector3(0.0f, yaw, 0.0f)), 0.1f);

    }
}
