using System;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioHandler : MonoBehaviour
{
    public List<AudioClip> AudioClips;
    // public List<float> ProbabilityPerClip;   can't make weighted selection work
    public AudioSource AudioSource;

    private void Start()
    {
        AudioSource.loop = false;
    }

    void Update()
    {
        AudioSource.volume = ((float)PlayerPrefsHandler.GetOption("MusicVolume")) / 100f * ((float)PlayerPrefsHandler.GetOption("GeneralVolume")) / 100f;
        if (!AudioSource.isPlaying)
        {
            AudioSource.clip = AudioClips[UnityEngine.Random.Range(0, AudioClips.Count)];
            AudioSource.Play();
        }
    }
}
