﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Svedlund
/// </summary>

public class PauseMenuWindowVisuals : MonoBehaviour
{
    private float originalCameraSpeedH;
    private float originalCameraSpeedV;
    private bool firstDisable = true;

    public CameraFollow cameraFollow;

    public void OnEnable()
    {
        Cursor.visible = true;
        if (!firstDisable) GameController.instance.state = GameController.GameControllerState.PAUSE;
        if (!firstDisable) AudioManager.instance.Play("Menu Open");

        //originalCameraSpeedH = cameraFollow.speedH;
        //originalCameraSpeedV = cameraFollow.speedV;
        //cameraFollow.speedH = 0;
        //cameraFollow.speedV = 0;
    }

    public void OnDisable()
    {
        //cameraFollow.speedH = originalCameraSpeedH;
        //cameraFollow.speedV = originalCameraSpeedV;

        if (!firstDisable) GameController.instance.state = GameController.GameControllerState.GAME;
        Cursor.visible = false;
        FindObjectOfType<PauseMenuWindow>().ResetPauseMenuWindow();
        if (!firstDisable) AudioManager.instance.Play("Menu Close");
        else firstDisable = false;


    }
}
