using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by: Svedlund
/// </summary>

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

    public float MiningRigExtractionRate;

    
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
            FindObjectOfType<PlayerController>().playerAirControllSpeed *= 1.3f;
            Debug.Log("Air Control upgrade has been applied, it's now at level " + upgradeIndex);
            Debug.Log("Air control is now " + FindObjectOfType<PlayerController>().playerAirControllSpeed);
        }

        else if (upgradeType == "Movement Speed")
        {
            currentMovementSpeedLevel = upgradeIndex;
            FindObjectOfType<PlayerController>().basePlayerSpeed *= 1.2f;
            Debug.Log("Movement Speed upgrade has been applied, it's now at level " + upgradeIndex);
            Debug.Log("basePlayerSpeed is now " + FindObjectOfType<PlayerController>().basePlayerSpeed);
        }

        else if (upgradeType == "Repair Rate")
        {
            currentMiningRigHealthLevel = upgradeIndex;
            //Debug.Log("Rig Health upgrade has been applied, it now at level " + upgradeIndex);
            Debug.Log("Rig Health is not yet implemented, you get nothing. But this int has been changed: " + upgradeIndex);
        }

        else if (upgradeType == "Extraction Rate")
        {
            currentExtractionRateLevel = upgradeIndex;
            MiningRigExtractionRate *= 0.7f;
            Debug.Log("Extraction Rate upgrade has been applied, it's now at level " + upgradeIndex);
            Debug.Log("miningRigExtractionRate is now " + MiningRigExtractionRate);
        }

        else if (upgradeType == "Shield Radius")
        {
            currentShieldRadiusLevel = upgradeIndex;
            //Debug.Log("Shield Radius upgrade has been applied, it now at level " + upgradeIndex);
            PortableShieldScale[] allPortableShields = FindObjectsOfType<PortableShieldScale>();
            foreach (PortableShieldScale shieldScaler in allPortableShields)
            {
                shieldScaler.SetShieldRadius();
            }
            // ATTENTION: For now currentShieldRadiusLevel can be 0-3. To add more levels you must update SetShieldRadius() in PortableShieldScale-script.
            Debug.Log("Shield Radius upgrade has been applied to all portable shields, it's now at level " + upgradeIndex);
            Debug.Log("currentShieldRadiusLevel is now " + currentShieldRadiusLevel);
        }

        else Debug.Log("The upgradeType-string you are looking for is " + upgradeType);
    }
}
