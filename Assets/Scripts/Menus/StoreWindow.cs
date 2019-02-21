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
    public Text buttonText;
    public Text topText;
    public GameObject upgradesList;
    public GameObject equipmentList;
    public GameObject creditsText;
    public GameObject upgradesButton;
    public GameObject equipmentButton;
    public GameObject suitUpgrades;
    public GameObject suitButton;
    public GameObject miningRigUpgrades;
    public GameObject miningRigButton;
    public GameObject portableShieldUpgrades;
    public GameObject portableShieldButton;
    public GameObject backToStoreFrontButton;
    public GameObject creditsTextPrefabPopUp;
    private float originalCameraSpeedH;
    private float originalCameraSpeedV;

    private CameraFollow cameraFollow;

    public GameObject miningRig;
    public GameObject portableShield;
    private PlayerController player;

    private Color redColor = new Color(207.0f / 255.0f, 88.0f / 255.0f, 69.0f / 255.0f);
    private Color blueColor = new Color(140.0f / 255.0f, 193.0f / 255.0f, 227.0f / 255.0f);

    public int miningRigCost;
    public int PortableShieldCost;

    [HideInInspector] public int currentAirControlLevel;
    [HideInInspector] public int currentMovementSpeedLevel;
    [HideInInspector] public int currentMiningRigHealthLevel;
    [HideInInspector] public int currentExtractionRateLevel;
    [HideInInspector] public int currentShieldRadiusLevel;

    public float MiningRigExtractionRate;

    //Note: This function is a little hard to grasp. It was originally built to handle the complete StoreWindow canvas logic,
    //but when more layers had to be implemented I had to rebuild it completely. This is the remains of that original system,
    //and handles the switching between UpgradesTab and EquipmentTab.

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
            if (upgradesTab.activeInHierarchy)
            {
                upgradesTab.SetActive(false);
                buttonText.text = "Back";
                equipmentButton.GetComponent<Image>().color = redColor;
                topText.text = "Buy Equipment";
                upgradesImage.SetActive(false);
                equipmentImage.SetActive(false);

                equipmentList.SetActive(true);
            }
            else
            {
                upgradesTab.SetActive(true);
                buttonText.text = buttonPressed;
                equipmentButton.GetComponent<Image>().color = blueColor;
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
            AudioManager.instance.Play("Store Buy");
        }
        else
        {
            InsufficientCredits();
            Debug.Log("Denying transaction");
        }
    }

    public void BuyPortableShield()
    {
        if (CheckAffordability(PortableShieldCost))
        {
            player = FindObjectOfType<PlayerController>();
            Instantiate(portableShield, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4 + player.transform.TransformDirection(Vector3.up) * 4, transform.rotation, null);
            Debug.Log("Buying portableShield");
            AudioManager.instance.Play("Store Buy");
        }
        else
        {
            InsufficientCredits();
            Debug.Log("Denying transaction");
        }
    }

    public bool CheckAffordability(int equipmentCost)
    {
        int playerCredits = FindObjectOfType<GameController>().playerCredits;
        if (playerCredits >= equipmentCost)
        {
            FindObjectOfType<GameController>().UpdateCredits(-equipmentCost);
            AudioManager.instance.Play("Store Buy");
            return true;
        }
        else return false;
    }

    public void InsufficientCredits()
    {
        iTween.PunchPosition(creditsText, Random.insideUnitCircle * 15f, 0.3f);
        iTween.PunchScale(creditsText, Random.insideUnitCircle * 1.5f, 0.3f);
        AudioManager.instance.Play("Store Error");
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
            FindObjectOfType<PlayerController>().playerAirControllSpeed *= 1.2f;
            Debug.Log("Air Control upgrade has been applied, it's now at level " + upgradeIndex);
            Debug.Log("Air control is now " + FindObjectOfType<PlayerController>().playerAirControllSpeed);
        }

        else if (upgradeType == "Movement Speed")
        {
            currentMovementSpeedLevel = upgradeIndex;
            FindObjectOfType<PlayerController>().basePlayerSpeed *= 1.1f;
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
        AudioManager.instance.Play("Store Buy");
    }

    public IEnumerator GainCreditsPopUp(int amountGained)
    {
        GameObject myPopUp = Instantiate(creditsTextPrefabPopUp, transform);
        Text myText = myPopUp.GetComponent<Text>();
        myText.text = "+" + amountGained;
        iTween.PunchPosition(myPopUp, Random.insideUnitCircle * 30f, 0.5f);
        iTween.PunchScale(myPopUp, Random.insideUnitCircle * 2f, 0.5f);
        myPopUp.GetComponentInChildren<Image>().CrossFadeAlpha(0f, 2f, true);
        myText.CrossFadeAlpha(0f, 2f, true);
        Vector3 destinationPosition = myPopUp.transform.position;
        destinationPosition.y += 100;
        iTween.MoveTo(myPopUp, destinationPosition, 10f);
        yield return new WaitForSeconds(2f);
        Destroy(myPopUp);
        yield return null;
    }

    public void ResetStoreWindow()
    {
        suitUpgrades.SetActive(false);
        suitButton.SetActive(true);
        miningRigUpgrades.SetActive(false);
        miningRigButton.SetActive(true);
        portableShieldUpgrades.SetActive(false);
        portableShieldButton.SetActive(true);
        backToStoreFrontButton.SetActive(true);
        upgradesList.SetActive(false);
        equipmentList.SetActive(false);
        upgradesTab.SetActive(true);
        equipmentTab.SetActive(true);
        upgradesButton.SetActive(true);
        SetTopText("Store");
        upgradesImage.SetActive(true);
        equipmentImage.SetActive(true);
        buttonText.text = "Equipment";
        equipmentButton.GetComponent<Image>().color = blueColor;
    }
}
