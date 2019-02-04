﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void Easy() {
        for (int i = 0; i < phaseSpecificsEasy.Length; i++) {
            GameController.instance.playerCredits = playerCreditsEasy;
            GameController.instance.PhaseSpecifics[i].totalTimeInPhase = phaseSpecificsEasy[i].totalTimeInPhase;
            GameController.instance.PhaseSpecifics[i].phaseBoxAmount = phaseSpecificsEasy[i].phaseBoxAmount;
            GameController.instance.PhaseSpecifics[i].timeBetweenMeteroids = phaseSpecificsEasy[i].timeBetweenMeteroids;
        }
    }
    public void Medium() {
        for (int i = 0; i < phaseSpecificsMedium.Length; i++) {
            GameController.instance.PhaseSpecifics = new PhaseSpecifics[phaseSpecificsMedium.Length];
            GameController.instance.playerCredits = playerCreditsMedium;
            GameController.instance.PhaseSpecifics[i].totalTimeInPhase = phaseSpecificsMedium[i].totalTimeInPhase;
            GameController.instance.PhaseSpecifics[i].phaseBoxAmount = phaseSpecificsMedium[i].phaseBoxAmount;
            GameController.instance.PhaseSpecifics[i].timeBetweenMeteroids = phaseSpecificsMedium[i].timeBetweenMeteroids;
        }
    }
    public void Hard() {
        for (int i = 0; i < phaseSpecificsHard.Length; i++) {
            GameController.instance.playerCredits = playerCreditsHard;
            GameController.instance.PhaseSpecifics[i].totalTimeInPhase = phaseSpecificsHard[i].totalTimeInPhase;
            GameController.instance.PhaseSpecifics[i].phaseBoxAmount = phaseSpecificsHard[i].phaseBoxAmount;
            GameController.instance.PhaseSpecifics[i].timeBetweenMeteroids = phaseSpecificsHard[i].timeBetweenMeteroids;
        }
    }
}
