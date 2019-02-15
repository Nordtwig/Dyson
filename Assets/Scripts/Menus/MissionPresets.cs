using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Created by: Noah Nordqvist
/// </summary>

public class MissionPresets : MonoBehaviour
{
    [Header("Easy")]
    public int playerCreditsEasy;
    public PhaseSpecifics[] phaseSpecificsEasy;
    [Header("Medium")]
    public int playerCreditsMedium;
    public PhaseSpecifics[] phaseSpecificsMedium;
    [Header("Hard")]
    public int playerCreditsHard;
    public PhaseSpecifics[] phaseSpecificsHard;

    public Text currentMission;

    public void Easy() {
        currentMission.text = "Difficulty: Easy";
        GameController.instance.phaseSpecifics = new PhaseSpecifics[phaseSpecificsEasy.Length];
        GameController.instance.playerCredits = playerCreditsEasy;

        for (int i = 0; i < phaseSpecificsEasy.Length; i++) {
            GameController.instance.phaseSpecifics[i] = new PhaseSpecifics();
            GameController.instance.phaseSpecifics[i].totalTimeInPhase = phaseSpecificsEasy[i].totalTimeInPhase;
            GameController.instance.phaseSpecifics[i].phaseBoxAmount = phaseSpecificsEasy[i].phaseBoxAmount;
            GameController.instance.phaseSpecifics[i].timeBetweenMeteroids = phaseSpecificsEasy[i].timeBetweenMeteroids;
        }
    }
    public void Medium() {
        currentMission.text = "Difficulty: Medium";
        GameController.instance.phaseSpecifics = new PhaseSpecifics[phaseSpecificsMedium.Length];
        GameController.instance.playerCredits = playerCreditsMedium;

        for (int i = 0; i < phaseSpecificsMedium.Length; i++) {
            GameController.instance.phaseSpecifics[i] = new PhaseSpecifics();
            GameController.instance.phaseSpecifics[i].totalTimeInPhase = phaseSpecificsMedium[i].totalTimeInPhase;
            GameController.instance.phaseSpecifics[i].phaseBoxAmount = phaseSpecificsMedium[i].phaseBoxAmount;
            GameController.instance.phaseSpecifics[i].timeBetweenMeteroids = phaseSpecificsMedium[i].timeBetweenMeteroids;
        }
    }
    public void Hard() {
        currentMission.text = "Difficulty: Hard";
        GameController.instance.phaseSpecifics = new PhaseSpecifics[phaseSpecificsHard.Length];
        GameController.instance.playerCredits = playerCreditsHard;

        for (int i = 0; i < phaseSpecificsHard.Length; i++) {
            GameController.instance.phaseSpecifics[i] = new PhaseSpecifics();
            GameController.instance.phaseSpecifics[i].totalTimeInPhase = phaseSpecificsHard[i].totalTimeInPhase;
            GameController.instance.phaseSpecifics[i].phaseBoxAmount = phaseSpecificsHard[i].phaseBoxAmount;
            GameController.instance.phaseSpecifics[i].timeBetweenMeteroids = phaseSpecificsHard[i].timeBetweenMeteroids;
        }
    }
}
