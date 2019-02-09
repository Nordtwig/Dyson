using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuWindow : MonoBehaviour
{
    
    public void RestartGame()
    {
        GetComponentInChildren<PauseMenuWindowVisuals>().gameObject.SetActive(false);
        GameController.instance.StartCoRestart();
    }

    public void GoToMainMenu()
    {
        GameController.instance.MainMenu();
    }

    public void QuitGame()
    {
        GameController.instance.Quit();
    }

    public void DisableOtherButtonsOnPrompt(GameObject promptPanel)
    {
        Button[] allButtons = FindObjectsOfType<Button>();

        foreach (Button button in allButtons)
        {
            button.interactable = false;
        }

        Button[] promtButtons = promptPanel.GetComponentsInChildren<Button>();

        foreach (Button button in promtButtons)
        {
            button.interactable = true;
        }
    }

    public void ReenableAllButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>();

        foreach (Button button in allButtons)
        {
            button.interactable = true;
        }
    }
}
