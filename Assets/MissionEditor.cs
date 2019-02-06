using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by: Noah Nordqvist
/// </summary>

public class MissionEditor : MonoBehaviour
{
    [Header("Mission Stats")]
    public int phases;
    public int quota;
    public int deadline;
    public int funds;
    public int forecast;
    public PhaseSpecifics[] phaseSpecifics;
    public InputField[] inputs;
    public GameObject addPhaseButton;
    public Text currentMission;

    Button phasePrefab;
    List<Button> phaseButtons;

    void Start() {
        phaseButtons = new List<Button>();    
    }

    public void GeneratePhaseSpecifics() {
        currentMission.text = "Current Mission: Custom";
        GameController.instance.phaseSpecifics = new PhaseSpecifics[phases];
        GameController.instance.playerCredits = funds;
        for (int i = 0; i < phaseSpecifics.Length; i++) {
            GameController.instance.phaseSpecifics[i] = new PhaseSpecifics();
            GameController.instance.phaseSpecifics[i].totalTimeInPhase = deadline;
            GameController.instance.phaseSpecifics[i].phaseBoxAmount = quota;
            GameController.instance.phaseSpecifics[i].timeBetweenMeteroids = forecast;
        }
    }

    public void OnTextInputChange(int inputIndex) {
        switch (inputIndex) {
            case 0:
                phases = int.Parse(inputs[inputIndex].text);
                Debug.Log(phases);
                break;
            case 1:
                quota = int.Parse(inputs[inputIndex].text);
                Debug.Log(quota);
                break;
            case 2:
                deadline = int.Parse(inputs[inputIndex].text);
                Debug.Log(deadline);
                break;
            case 3:
                funds = int.Parse(inputs[inputIndex].text);
                Debug.Log(funds);
                break;
            case 4:
                forecast = int.Parse(inputs[inputIndex].text);
                Debug.Log(forecast);
                break;
            default:
                break;
        }
    }

    public void AddPhaseButton() {
        Button newButton = Instantiate(phasePrefab, transform.GetChild(2));
        phaseButtons.Add(newButton);
        newButton.GetComponentInChildren<Text>().text = phaseButtons.IndexOf(newButton).ToString();
        addPhaseButton.transform.SetSiblingIndex(phaseButtons.IndexOf(newButton) + 1);
    }
}
