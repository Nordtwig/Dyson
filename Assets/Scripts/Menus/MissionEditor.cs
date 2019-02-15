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
    public List<PhaseSpecifics> phaseSpecifics;
    public InputField[] inputs;
    public GameObject addPhaseButton;
    public Button phasePrefab;
    public Text currentMission;

    List<Button> phaseButtons;
    List<int> phaseValues;
    List<List<int>> phaseValuesLists;

    void Start() {
        phaseButtons = new List<Button>();
        phaseValues = new List<int>();
        phaseValuesLists = new List<List<int>>();
    }

    public void GeneratePhaseSpecifics() {
        currentMission.text = "Current Mission: Custom";
        //GameController.instance.phaseSpecifics = new PhaseSpecifics[phases]; // TODO Call this onsceneloaded with values from mission editor.
        //GameController.instance.playerCredits = funds;
        //for (int i = 0; i < phaseSpecifics.Length; i++) {
        //    GameController.instance.phaseSpecifics[i] = new PhaseSpecifics();
        //    GameController.instance.phaseSpecifics[i].totalTimeInPhase = deadline;
        //    GameController.instance.phaseSpecifics[i].phaseBoxAmount = quota;
        //    GameController.instance.phaseSpecifics[i].timeBetweenMeteroids = forecast;
        //}
        foreach (InputField input in inputs) {
            int inputValue;
            if (string.IsNullOrEmpty(input.text))
                inputValue = 0;
            else 
                inputValue = int.Parse(input.text);
            //if (inputValue != null) // TODO Add different rules to different fields. Can't have 0 box quota, etc.
            phaseValues.Add(inputValue);
        }
        phaseValuesLists.Add(phaseValues);
    }

    public void OnTextInputChange(int inputIndex) {
        switch (inputIndex) {
            case 0:
                quota = int.Parse(inputs[inputIndex].text);
                Debug.Log(quota);
                break;
            case 1:
                deadline = int.Parse(inputs[inputIndex].text);
                Debug.Log(deadline);
                break;
            case 2:
                funds = int.Parse(inputs[inputIndex].text);
                Debug.Log(funds);
                break;
            case 3:
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
        newButton.GetComponentInChildren<Text>().text = (phaseButtons.IndexOf(newButton)).ToString();
        newButton.onClick.AddListener(() => PhaseButtonHandler(phaseButtons.IndexOf(newButton)));
        addPhaseButton.transform.SetSiblingIndex(phaseButtons.IndexOf(newButton) + 1);
        //PhaseButtonHandler(phaseButtons.IndexOf(newButton));
    }

    void PhaseButtonHandler(int phaseButton) {
        List<int> tempValues;

        if (phaseValuesLists.Count <= 0) {
            tempValues = new List<int>();
            tempValues.Add(1);
            tempValues.Add(2);
            tempValues.Add(3);
            tempValues.Add(4);
        }
        else {
            tempValues = phaseValuesLists[phaseButton];
        }
        foreach (InputField input in inputs) {
            
            if (string.IsNullOrEmpty(input.text))
                input.text = "0";
            else
                input.text = tempValues[phaseButton].ToString();
        }
    }

    void CheckFieldRules(int field, int inputValue) {
        //switch (field) {
        //    case 0:
        //        phases = int.Parse(inputs[inputIndex].text);
        //        Debug.Log(phases);
        //        break;
        //    case 1:
        //        quota = int.Parse(inputs[inputIndex].text);
        //        Debug.Log(quota);
        //        break;
        //    case 2:
        //        deadline = int.Parse(inputs[inputIndex].text);
        //        Debug.Log(deadline);
        //        break;
        //    case 3:
        //        funds = int.Parse(inputs[inputIndex].text);
        //        Debug.Log(funds);
        //        break;
        //    case 4:
        //        forecast = int.Parse(inputs[inputIndex].text);
        //        Debug.Log(forecast);
        //        break;
        //    default:
        //        break;
        //}
    }
}
