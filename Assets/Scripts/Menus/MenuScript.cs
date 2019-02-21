using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

/// <summary>
/// Christoffer Brandt
/// Noah Nordqvist
/// Svedlund
/// slight addition - Heimer
/// </summary>

public class MenuScript : MonoBehaviour
{
    public GameObject[] views;
    public GameObject[] menus;
    public GameObject currMenu;
    public GameObject currView;
    private bool firstInitialisation = true;

    private Animator animator;

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
        SetCurrentMenu(0);
        animator = FindObjectOfType<Animator>();
        currView = views[0];
        currMenu = menus[0];
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
        MusicManager.instance.CrossfadeMusic(MusicManager.instance.menuMusicLoop, MusicManager.instance.musicIntro, true);
        SceneManager.LoadScene(targetScene);
        Debug.Log("Game START");
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

    public void SetCurrentMenu(int menu)
    {
        currMenu = menus[menu];
        for (int i = 0; i < menus.Length; i++)
        {
            if (firstInitialisation)
                menus[i].GetComponent<CanvasGroup>().alpha = 0;
            else if (menus[i] != currMenu)
                StartCoroutine(CrossfadeCanvasAlpha(menus[i], 0));
        }
        if (firstInitialisation)
        {
            currMenu.GetComponent<CanvasGroup>().alpha = 1;
            firstInitialisation = false;
        }
        else
            StartCoroutine(CrossfadeCanvasAlpha(currMenu, 1));
    }

    private IEnumerator CrossfadeCanvasAlpha(GameObject canvasToFade, float fadeTargetAlpha)
    {
        canvasToFade.GetComponent<CanvasGroup>().interactable = false;
        if (fadeTargetAlpha == 0)
        {
            float x = 1;
            while (x > 0)
            {
                canvasToFade.GetComponent<CanvasGroup>().alpha -= 0.25f;
                x -= 0.25f;
                yield return new WaitForSeconds(0.1f);
            }
            canvasToFade.GetComponent<CanvasGroup>().interactable = true;
            yield return null;
        }
        else if (fadeTargetAlpha == 1)
        {
            float x = 0;
            while (x < 1)
            {
                canvasToFade.GetComponent<CanvasGroup>().alpha += 0.25f;
                x += 0.25f;
                yield return new WaitForSeconds(0.1f);
            }
            canvasToFade.GetComponent<CanvasGroup>().interactable = true;
            yield return null;
        }
    }
}
