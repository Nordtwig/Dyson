using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenuWindow : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public AudioMixer audioMixer;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;


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

        Debug.Log("Disabling buttons");
    }

    public void ReenableAllButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>();

        foreach (Button button in allButtons)
        {
            button.interactable = true;
        }
    }

    //public void ToggleFullscreen()
    //{
    //    if (fullscreenToggle.isOn)
    //    {

    //    }
    //}

    public void ChangeMusicVolume()
    {
        if (musicVolumeSlider.value <= -40)
        {
            audioMixer.SetFloat("musicVolume", -80);
        }
        else audioMixer.SetFloat("musicVolume", musicVolumeSlider.value);
    }

    public void ChangeSoundVolume()
    {
        if (soundVolumeSlider.value <= -40)
        {
            audioMixer.SetFloat("soundVolume", -80);
        }
        else audioMixer.SetFloat("soundVolume", soundVolumeSlider.value);
    }
}
