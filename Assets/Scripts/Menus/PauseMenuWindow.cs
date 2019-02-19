using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Created by: Svedlund
/// </summary>

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
    public GameObject[] promptPanels;

    private Color mutedColor = new Color(54.0f / 255.0f, 54.0f / 255.0f, 54.0f / 255.0f);
    private Color nonMutedColor = new Color(215.0f / 255.0f, 215.0f / 255.0f, 215.0f / 255.0f);



    public void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        musicVolumeSlider.value = FindObjectOfType<AudioManager>().GetMusicVolume();
        soundVolumeSlider.value = FindObjectOfType<AudioManager>().GetSoundVolume();
    }

    

    public void RestartGame()
    {
        GetComponentInChildren<PauseMenuWindowVisuals>().gameObject.SetActive(false);
        GameController.instance.Restart();
    }

    public void GoToMainMenu()
    {
        GameController.instance.StartCoMainMenu();
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

    public bool PromptPanelDeactivated()
    {
        foreach (GameObject promptPanel in promptPanels)
        {
            if (promptPanel.activeInHierarchy)
            {
                promptPanel.SetActive(false);
                ReenableAllButtons();
                Debug.Log("A prompt has been deactivated, back to pause menu");
                return true;
            }
        }
        return false;
    }

    public void ResetPauseMenuWindow()
    {
        
        {
            backButton.SetActive(false);
            quitButton.SetActive(true);
            contentPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }
    }
}
