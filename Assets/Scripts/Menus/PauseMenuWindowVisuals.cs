using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuWindowVisuals : MonoBehaviour
{
    private float originalCameraSpeedH;
    private float originalCameraSpeedV;
    private bool firstDisable = true;

    public CameraFollow cameraFollow;

    public void OnEnable()
    {
        Cursor.visible = true;
        GameController.instance.state = GameController.GameControllerState.PAUSE;
        AudioManager.instance.Play("Menu Open");

        //originalCameraSpeedH = cameraFollow.speedH;
        //originalCameraSpeedV = cameraFollow.speedV;
        //cameraFollow.speedH = 0;
        //cameraFollow.speedV = 0;
    }

    public void OnDisable()
    {
        //cameraFollow.speedH = originalCameraSpeedH;
        //cameraFollow.speedV = originalCameraSpeedV;

        GameController.instance.state = GameController.GameControllerState.GAME;
        Cursor.visible = false;
        FindObjectOfType<PauseMenuWindow>().ResetPauseMenuWindow();
        if (!firstDisable) AudioManager.instance.Play("Menu Close");
        else firstDisable = false;


    }
}
