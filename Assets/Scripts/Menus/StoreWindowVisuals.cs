using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Svedlund
/// </summary>

public class StoreWindowVisuals : MonoBehaviour
{
    private bool firstDisable = true;

    public CameraFollow cameraFollow;
    

    public void OnEnable()
    {
        if (!firstDisable)
            GameController.instance.state = GameController.GameControllerState.STOREWINDOW;
        Cursor.visible = true;
        if (!firstDisable)
            AudioManager.instance.Play("Menu Open");
    }

    public void OnDisable()
    {
        if (!firstDisable)
            GameController.instance.state = GameController.GameControllerState.GAME;
        Cursor.visible = false;
        FindObjectOfType<StoreWindow>().ResetStoreWindow();
        if (!firstDisable)
            AudioManager.instance.Play("Menu Close");
        else
            firstDisable = false;
    }
}
