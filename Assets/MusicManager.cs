using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicIntro;
    private AudioSource musicLoop;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        musicIntro = audios[0];
        musicLoop = audios[1];
        musicIntro.Play();
        musicLoop.PlayDelayed(musicIntro.clip.length);
    }

}
