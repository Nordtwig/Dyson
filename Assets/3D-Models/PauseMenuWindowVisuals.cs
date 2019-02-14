using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuWindowVisuals : MonoBehaviour
{
    private float originalCameraSpeedH;
    private float originalCameraSpeedV;

    public CameraFollow cameraFollow;

    public void OnEnable()
    {
        Cursor.visible = true;
        GameController.instance.state = GameController.GameControllerState.PAUSE;

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
    }
}
