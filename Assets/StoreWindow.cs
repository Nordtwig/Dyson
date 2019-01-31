using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreWindow : MonoBehaviour
{
    public GameObject upgradesTab;
    public GameObject objectsTab;
    public GameObject upgradesImage;
    public GameObject objectsImage;
    private Text buttonText;
    public GameObject upgradesList;
    public GameObject objectsList;
    private float originalCameraSpeedH;
    private float originalCameraSpeedV;


    public void OnEnable()
    {
        originalCameraSpeedH = FindObjectOfType<CameraFollow>().speedH;
        originalCameraSpeedV = FindObjectOfType<CameraFollow>().speedV;
        FindObjectOfType<CameraFollow>().speedH = 0;
        FindObjectOfType<CameraFollow>().speedV = 0;
    }

    public void OnDisable()
    {
        FindObjectOfType<CameraFollow>().speedH = originalCameraSpeedH;
        FindObjectOfType<CameraFollow>().speedV = originalCameraSpeedV;

        GameController.instance.state = GameController.GameControllerState.GAME;
        Cursor.visible = false;
    }

    public void ToggleCanvases(string buttonPressed)
    {
        if (buttonPressed == "Upgrades")
        {
            buttonText = upgradesTab.GetComponentInChildren<Text>();

            if (objectsTab.activeInHierarchy)
            {
                objectsTab.SetActive(false);
                buttonText.text = "Back";
                upgradesImage.SetActive(false);
                objectsImage.SetActive(false);

                upgradesList.SetActive(true);
            }
            else
            {
                objectsTab.SetActive(true);
                buttonText.text = buttonPressed;
                upgradesImage.SetActive(true);
                objectsImage.SetActive(true);

                upgradesList.SetActive(false);
            }
        }

        else if(buttonPressed == "Objects")
        {
            buttonText = objectsTab.GetComponentInChildren<Text>();

            if (upgradesTab.activeInHierarchy)
            {
                upgradesTab.SetActive(false);
                buttonText.text = "Back";
                upgradesImage.SetActive(false);
                objectsImage.SetActive(false);
            }
            else
            {
                upgradesTab.SetActive(true);
                buttonText.text = buttonPressed;
                upgradesImage.SetActive(true);
                objectsImage.SetActive(true);
            }

        }
    }

    public void DisplayAvailableOptions()
    {
        
    }
}
