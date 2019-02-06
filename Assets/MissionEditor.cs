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
    public Text currentMission;

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
        if (inputIndex == 0) {
            phases = int.Parse(inputs[inputIndex].text);
            Debug.Log(phases);
        }
        if (inputIndex == 1) {
            quota = int.Parse(inputs[inputIndex].text);
            Debug.Log(quota);
        }
        if (inputIndex == 2) {
            deadline = int.Parse(inputs[inputIndex].text);
            Debug.Log(deadline);
        }
        if (inputIndex == 3) {
            funds = int.Parse(inputs[inputIndex].text);
            Debug.Log(funds);
        }
        if (inputIndex == 4) {
            forecast = int.Parse(inputs[inputIndex].text);
            Debug.Log(forecast);
        }

    }
}
