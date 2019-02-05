using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public string upgradeType;
    public int upgradeIndex = 1;
    public int upgradeMaxIndex = 3;
    public Text upgradeText;
    private GameController gameController;
    private int playerCredits;
    public int creditsCost;
    public float CreditsCostMultiplier;
    public Text costText;

    public void Start()
    {
        gameController = FindObjectOfType<GameController>();

        costText.text = creditsCost.ToString();
        name = upgradeType;
        upgradeText.text = upgradeType + " " + upgradeIndex;
    }

    public void DebugUpgradeBought(string upgradeBought)
    {
        playerCredits = gameController.playerCredits;
        if (playerCredits >= creditsCost)
        {
            Debug.Log("You have bought " + upgradeText.text);
            FindObjectOfType<StoreWindow>().SetUpgradeLevel(upgradeType, upgradeIndex);
            upgradeIndex++;
            upgradeText.text = upgradeType + " " + upgradeIndex;

            if (upgradeIndex > upgradeMaxIndex)
            {
                gameObject.SetActive(false);
            }

            gameController.UpdateCredits(-creditsCost);
            UpdateCost();
        }
        else
        {
            Debug.Log("You cannot afford this, you poor bugger!");
        }
    }

    public void UpdateCost()
    {
        creditsCost = (int) (creditsCost * CreditsCostMultiplier);
        costText.text = creditsCost.ToString();
    }
}
