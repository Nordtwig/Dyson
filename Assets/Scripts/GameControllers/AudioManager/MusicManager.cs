using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script Creator Heimer
/// </summary>
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public bool startedInMenu ;

    public AudioSource musicIntro;
    public AudioSource musicLoop;
    public AudioSource menuMusicLoop;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        musicIntro = audios[0];
        musicLoop = audios[1];
        menuMusicLoop = audios[2];

        if (startedInMenu)
        {
            menuMusicLoop.Play();
            iTween.AudioFrom(gameObject, iTween.Hash(
                "audiosource", menuMusicLoop,
                "volume", 0f,
                "time", 2f
            ));
        }
        else
        {
            musicIntro.Play();
            iTween.AudioFrom(gameObject, iTween.Hash(
                "audiosource", musicIntro,
                "volume", 0f,
                "time", 2f
            ));
        }
        //musicLoop.PlayDelayed(musicIntro.clip.length);

    }

    // I know this is a strange way to do this
    public void CrossfadeMusic(AudioSource fromMusic, AudioSource toMusic, bool playDelayedLoop)
    {
        StopCoroutine(CoCrossfadeMusic(fromMusic, toMusic, playDelayedLoop));
        StartCoroutine(CoCrossfadeMusic(fromMusic, toMusic, playDelayedLoop));
    }

    public IEnumerator CoCrossfadeMusic(AudioSource fromMusic, AudioSource toMusic, bool fromMenu)
    {

        // fades the audio of the current music to 0
        iTween.AudioTo(gameObject, iTween.Hash(
            "audiosource", fromMusic,
            "volume", 0f,
            "time", 1f
        ));

        yield return new WaitForSeconds(1f);

        toMusic.Play();
        if (fromMenu)
        {
            musicLoop.volume = 1;
            musicLoop.PlayDelayed(toMusic.clip.length);
            fromMenu = false;
        }
        // don't look i'm hideous :(
        else
        {
            iTween.AudioTo(gameObject, iTween.Hash(
                "audiosource", musicLoop,
                "volume", 0f,
                "time", 1f
            ));
            fromMenu = true;
        }
        fromMusic.Stop();
        fromMusic.volume = 1;

        //sets the audio of the new music track to 0 and fades it back to 1
        iTween.AudioFrom(gameObject, iTween.Hash(
            "audiosource", toMusic,
            "volume", 0f,
            "time", 2f
        ));
        yield return null;
    }

}
