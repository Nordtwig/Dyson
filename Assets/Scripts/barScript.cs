using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Christoffer Brandt
/// </summary>

public class barScript : MonoBehaviour
{
    public Slider ProgressBar;
    public float maxBoxes = 5, percentageNumber;
    public Text Percentage, NextTurnText;

    void Start()
    {
        ProgressBar.value = (float) GameController.instance.GetAmountOfDeliveredBoxes() / maxBoxes;
        percentageNumber = (GameController.instance.GetAmountOfDeliveredBoxes() / maxBoxes) * 100;
        Percentage.text = percentageNumber.ToString();
    }

    void ProgressBarUpdate()
    {
        ProgressBar.value = GameController.instance.GetAmountOfDeliveredBoxes() / maxBoxes;
        if (GameController.instance.GetAmountOfDeliveredBoxes() <= 5)
        {
            NextTurn();
        }
    }

    void NextTurn()
    {
        NextTurnText.text = NextTurnText.ToString();
    }
}
