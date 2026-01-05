using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuAudioHandler : MonoBehaviour
{
    public List<AudioClip> AudioClips;
    public int StartupAudioKey;
    public int LoopAudioKey;
    public AudioSource AudioSource;

    void Start()
    {
        if (StartupAudioKey < AudioClips.Count)
        {
            AudioSource.clip = AudioClips[StartupAudioKey];
            AudioSource.loop = false;
            AudioSource.Play();
        }
    }

    void Update()
    {
        AudioSource.volume = ((float)PlayerPrefsHandler.GetOption("MusicVolume"))/100f * ((float)PlayerPrefsHandler.GetOption("GeneralVolume"))/100f;
        if (!AudioSource.isPlaying && AudioSource.clip == AudioClips[StartupAudioKey] && LoopAudioKey < AudioClips.Count)
        {
            AudioSource.clip = AudioClips[LoopAudioKey];
            AudioSource.loop = true;
            AudioSource.Play();
        }
    }
}
