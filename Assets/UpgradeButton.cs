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

    public void Start()
    {
        name = upgradeType;
        upgradeText.text = upgradeType + " " + upgradeIndex;
    }

    public void DebugUpgradeBought(string upgradeBought)
    {
        Debug.Log("You have bought " + upgradeText.text);
        upgradeIndex++;
        upgradeText.text = upgradeType + " " + upgradeIndex;

        if (upgradeIndex > upgradeMaxIndex)
        {
            gameObject.SetActive(false);
        }
    }
}
