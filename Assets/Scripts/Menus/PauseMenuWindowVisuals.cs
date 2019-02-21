using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Svedlund
/// </summary>

public class PauseMenuWindowVisuals : MonoBehaviour
{
    private bool firstDisable = true;

    public CameraFollow cameraFollow;

    public void OnEnable()
    {
        Cursor.visible = true;
        if (!firstDisable) GameController.instance.state = GameController.GameControllerState.PAUSE;
        if (!firstDisable) AudioManager.instance.Play("Menu Open");
    }

    public void OnDisable()
    {
        if (!firstDisable) GameController.instance.state = GameController.GameControllerState.GAME;
        Cursor.visible = false;
        FindObjectOfType<PauseMenuWindow>().ResetPauseMenuWindow();
        if (!firstDisable) AudioManager.instance.Play("Menu Close");
        else firstDisable = false;
    }
}
