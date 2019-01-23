using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Christoffer Brandt, Modified by Robin
/// </summary>

public class BarScript : MonoBehaviour
{
    private Slider progressBar;
    private Text percentage;

    private float percentageNumber;

    void Start()
    {
        progressBar = FindObjectOfType<Slider>();
        percentage = GameObject.Find("Percentage").GetComponent<Text>();
        progressBar.value = (float) GameController.instance.GetAmountOfDeliveredBoxes() / GameController.instance.phaseAmount;
        percentageNumber = (GameController.instance.GetAmountOfDeliveredBoxes() / GameController.instance.phaseAmount) * 100;
        percentage.text = percentageNumber.ToString() + "% of Boxes delivered";
    }

    public void ProgressBarUpdate()
    {
        progressBar.value = (float)GameController.instance.GetAmountOfDeliveredBoxes() / GameController.instance.phaseAmount;
        percentageNumber = (GameController.instance.GetAmountOfDeliveredBoxes()*100) / GameController.instance.phaseAmount;
        if (percentageNumber != 100)
        {
            string temp = percentageNumber.ToString();
            percentage.text = temp + "% of Boxes delivered";
        }
        else
        {
            percentage.text = "Ready to launch!";
        }
        
    }

    public void InvokeProgressBarUpdate(int time)
    {
        Invoke("ProgressBarUpdate", time);
    }

}
