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
    public Text musicMuted;
    public Text soundMuted;
    public GameObject contentPanel;
    public GameObject settingsPanel;
    public GameObject backButton;
    public GameObject quitButton;

    private Color mutedColor = new Color(54.0f / 255.0f, 54.0f / 255.0f, 54.0f / 255.0f);
    private Color nonMutedColor = new Color(215.0f / 255.0f, 215.0f / 255.0f, 215.0f / 255.0f);



    public void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
    }

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

    public void ToggleFullscreen()
    {
        if (fullscreenToggle.isOn)
        {
            Screen.fullScreen = true;
        }
        else Screen.fullScreen = false;
    }

    public void ChangeMusicVolume()
    {
        if (musicVolumeSlider.value <= -40)
        {
            audioMixer.SetFloat("musicVolume", -80);
            musicMuted.color = mutedColor;
        }
        else
        {
            audioMixer.SetFloat("musicVolume", musicVolumeSlider.value);
            musicMuted.color = nonMutedColor;
        }
    }

    public void ChangeSoundVolume()
    {
        if (soundVolumeSlider.value <= -40)
        {
            audioMixer.SetFloat("soundVolume", -80);
            soundMuted.color = mutedColor;
        }
        else
        {
            audioMixer.SetFloat("soundVolume", soundVolumeSlider.value);
            soundMuted.color = nonMutedColor;
        }
    }

    public void ResetPauseMenuWindow()
    {
        backButton.SetActive(false);
        quitButton.SetActive(true);
        contentPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
