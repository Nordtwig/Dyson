using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// Made by Brackeys, Stolen and Modified by Heimer
/// </summary>

[System.Serializable]
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

    [Range(0f, 1f)]
    public float spatialBlend = 0f;

    public bool sound3d = false;

    public bool playOnAwake = false;

	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

}
