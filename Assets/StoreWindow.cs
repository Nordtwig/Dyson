using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreWindow : MonoBehaviour
{
    public GameObject upgradesTab;
    public GameObject equipmentTab;
    public GameObject upgradesImage;
    public GameObject equipmentImage;
    private Text buttonText;
    public Text topText;
    public GameObject upgradesList;
    public GameObject equipmentList;
    private float originalCameraSpeedH;
    private float originalCameraSpeedV;

    private CameraFollow cameraFollow;

    public GameObject miningRig;
    public GameObject portableShield;
    private PlayerController player;

    public int miningRigCost;
    public int PortableShieldCost;

    [HideInInspector] public int currentAirControlLevel;
    [HideInInspector] public int currentMovementSpeedLevel;
    [HideInInspector] public int currentMiningRigHealthLevel;
    [HideInInspector] public int currentExtractionRateLevel;
    [HideInInspector] public int currentShieldRadiusLevel;


    public void OnEnable()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();

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

    public void ToggleCanvases(string buttonPressed)
    {
        if (buttonPressed == "Upgrades")
        {
            if (equipmentTab.activeInHierarchy)
            {
                equipmentTab.SetActive(false);
                topText.text = "Buy Upgrades";
                upgradesImage.SetActive(false);
                equipmentImage.SetActive(false);

                upgradesList.SetActive(true);
            }
            else
            {
                equipmentTab.SetActive(true);
                upgradesImage.SetActive(true);
                equipmentImage.SetActive(true);

                upgradesList.SetActive(false);
            }
        }

        else if(buttonPressed == "Equipment")
        {
            buttonText = equipmentTab.GetComponentInChildren<Text>();

            if (upgradesTab.activeInHierarchy)
            {
                upgradesTab.SetActive(false);
                buttonText.text = "Back";
                topText.text = "Buy Equipment";
                upgradesImage.SetActive(false);
                equipmentImage.SetActive(false);

                equipmentList.SetActive(true);
            }
            else
            {
                upgradesTab.SetActive(true);
                buttonText.text = buttonPressed;
                topText.text = "Store";
                upgradesImage.SetActive(true);
                equipmentImage.SetActive(true);

                equipmentList.SetActive(false);
            }

        }
    }

    public void BuyMiningRig()
    {
        if (CheckAffordability(miningRigCost))
        {
            player = FindObjectOfType<PlayerController>();
            Instantiate(miningRig, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
            Debug.Log("Buying miningRig");
        }
        else Debug.Log("Denying transaction");
    }

    public void BuyPortableShield()
    {
        if (CheckAffordability(PortableShieldCost))
        {
            player = FindObjectOfType<PlayerController>();
            Instantiate(portableShield, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
            Debug.Log("Buying portableShield");
        }
        else Debug.Log("Denying transaction");
    }

    public bool CheckAffordability(int equipmentCost)
    {
        int playerCredits = FindObjectOfType<GameController>().playerCredits;
        if (playerCredits >= equipmentCost)
        {
            FindObjectOfType<GameController>().UpdateCredits(-equipmentCost);
            return true;
        }
        else return false;
    }

    public void SetTopText(string text)
    {
        topText.text = text;
    }

    public void SetUpgradeLevel(string upgradeType, int upgradeIndex)
    {
        if (upgradeType == "Air Control")
        {
            currentAirControlLevel = upgradeIndex;
            Debug.Log("Air Control upgrade has been applied, it now at level " + upgradeIndex);
        }

        else if (upgradeType == "Movement Speed")
        {
            currentMovementSpeedLevel = upgradeIndex;
            Debug.Log("Movement Speed upgrade has been applied, it now at level " + upgradeIndex);
        }

        else if (upgradeType == "Mining Rig Health")
        {
            currentMiningRigHealthLevel = upgradeIndex;
            Debug.Log("Rig Health upgrade has been applied, it now at level " + upgradeIndex);
        }

        else if (upgradeType == "Extraction Rate")
        {
            currentExtractionRateLevel = upgradeIndex;
            Debug.Log("Rig Health upgrade has been applied, it now at level " + upgradeIndex);
        }

        else if (upgradeType == "Shield Radius")
        {
            currentShieldRadiusLevel = upgradeIndex;
            Debug.Log("Shield Radius upgrade has been applied, it now at level " + upgradeIndex);
        }

        else Debug.Log("The upgradeType-string you are looking for is " + upgradeType);
    }
}
