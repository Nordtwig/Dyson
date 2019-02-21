using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by: Svedlund
/// </summary>

public class UpgradeButton : MonoBehaviour
{
    public string upgradeType;
    public int upgradeIndex = 1;
    public int upgradeMaxIndex = 3;
    public Text upgradeText;
    private GameController gameController;
    private StoreWindow storeWindow;
    private int playerCredits;
    public int creditsCost;
    public float CreditsCostMultiplier;
    public Text costText;

    public void Start()
    {
        gameController = FindObjectOfType<GameController>();
        storeWindow = FindObjectOfType<StoreWindow>();

        costText.text = creditsCost.ToString();
        name = upgradeType;
        upgradeText.text = upgradeType + " " + upgradeIndex;
    }

    //TODO rename this function, and remove upgradeBought string. Leaving it as is for now, due to presentation tomorrow
    public void DebugUpgradeBought(string upgradeBought)
    {
        playerCredits = gameController.playerCredits;
        if (playerCredits >= creditsCost)
        {
            Debug.Log("You have bought " + upgradeText.text);
            FindObjectOfType<StoreWindow>().SetUpgradeLevel(upgradeType, upgradeIndex);
            upgradeIndex++;
            gameController.UpdateCredits(-creditsCost);

            if (upgradeIndex > upgradeMaxIndex)
            {
                upgradeIndex--;
                //gameObject.SetActive(false);
                GetComponent<Button>().interactable = false;
                //costText.text = "";
                costText.gameObject.SetActive(false);
            }
            else UpdateCost();

            upgradeText.text = upgradeType + " " + upgradeIndex;
        }
        else
        {
            storeWindow.InsufficientCredits();
            Debug.Log("You cannot afford this, you poor bugger!");
        }
    }

    public void UpdateCost()
    {
        creditsCost = (int) (creditsCost * CreditsCostMultiplier);
        costText.text = creditsCost.ToString();
    }
}
