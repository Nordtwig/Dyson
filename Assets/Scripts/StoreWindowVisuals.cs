using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Svedlund
/// </summary>

public class StoreWindowVisuals : MonoBehaviour
{
    private float originalCameraSpeedH;
    private float originalCameraSpeedV;

    public CameraFollow cameraFollow;

    public void OnEnable()
    {
        //cameraFollow = FindObjectOfType<CameraFollow>();

        originalCameraSpeedH = cameraFollow.speedH;
        originalCameraSpeedV = cameraFollow.speedV;
        cameraFollow.speedH = 0;
        cameraFollow.speedV = 0;
    }

    public void OnDisable()
    {
        cameraFollow.speedH = originalCameraSpeedH;
        cameraFollow.speedV = originalCameraSpeedV;

        GameController.instance.state = GameController.GameControllerState.GAME;
        Cursor.visible = false;
    }
}
