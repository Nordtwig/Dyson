using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by Christoffer Brandt, Modified by Robin
/// </summary>

public class ProgressBarScript : MonoBehaviour
{
    public static ProgressBarScript instance;

    private Slider progressBar;
    private Text percentage;
    private Text boxAmount;
    private int boxesDelivered;
    private int boxesNeeded;

    private float percentageNumber;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            instance.enabled = true;
        }
    }

    void Start()
    {
        progressBar = GetComponent<Slider>();
        percentage = GameObject.Find("Percentage").GetComponent<Text>();
        boxAmount = GameObject.Find("BoxAmount").GetComponent<Text>();
        boxesDelivered = GameController.instance.GetAmountOfDeliveredBoxes();
        boxesNeeded = GameController.instance.phaseBoxAmount;
        progressBar.value = (float)GameController.instance.GetAmountOfDeliveredBoxes() / GameController.instance.phaseBoxAmount;
        percentageNumber = (GameController.instance.GetAmountOfDeliveredBoxes() / GameController.instance.phaseBoxAmount) * 100;
        percentage.text = percentageNumber.ToString() + "%";
        boxAmount.text = GameController.instance.GetAmountOfDeliveredBoxes().ToString() + "/" + GameController.instance.phaseBoxAmount;
    }

    public void ProgressBarUpdate()
    {
        if (progressBar)
        {
            progressBar.value = (float)GameController.instance.GetAmountOfDeliveredBoxes() / GameController.instance.phaseBoxAmount;
            percentageNumber = (GameController.instance.GetAmountOfDeliveredBoxes() * 100) / GameController.instance.phaseBoxAmount;
            if (percentageNumber < 100)
            {
                string temp = percentageNumber.ToString();
                percentage.text = temp + "%";
                boxAmount.text = GameController.instance.GetAmountOfDeliveredBoxes().ToString() + "/" + GameController.instance.phaseBoxAmount;
            }
            else
            {
                percentage.text = "Ready to launch!";
                boxAmount.text = GameController.instance.GetAmountOfDeliveredBoxes().ToString() + "/" + GameController.instance.phaseBoxAmount;
            }
        }

    }

    public void InvokeProgressBarUpdate(int time)
    {
        Invoke("ProgressBarUpdate", time);
    }

}
