using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Christoffer Brandt
/// Noah Nordqvist
/// </summary>

public class MenuScript : MonoBehaviour
{
    public GameObject[] views;
    public GameObject currView;

    public Toggle fullscreenToggle;
    public AudioMixer audioMixer;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    public Text musicMuted;
    public Text soundMuted;

    private Color mutedColor = new Color(54.0f / 255.0f, 54.0f / 255.0f, 54.0f / 255.0f);
    private Color nonMutedColor = new Color(215.0f / 255.0f, 215.0f / 255.0f, 215.0f / 255.0f);

    // Start is called before the first frame update
    void Start()
    {
        currView = views[0];
        fullscreenToggle.isOn = Screen.fullScreen;
        musicVolumeSlider.value = FindObjectOfType<AudioManager>().GetMusicVolume();
        soundVolumeSlider.value = FindObjectOfType<AudioManager>().GetSoundVolume();
    }

    /*================================== Main Menu ======================================*/

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame(string targetScene)
    {
        if (GameController.instance.phaseSpecifics.Length == 0)
        {
            FindObjectOfType<MissionPresets>().Medium();
        }
        SceneManager.LoadScene(targetScene);
    }
    /*================================== Play Menu ======================================*/

    public void GoToView(int view) {
        currView.SetActive(false);
        currView = views[view];
        currView.SetActive(true);
    }

    //public void ReturnToView(int view) {
    //    currView.SetActive(false);
    //    currView = views[view];
    //    currView.SetActive(true);
    //}

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
}
