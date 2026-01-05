using UnityEngine;

public class ButtonClickAudioSourceHandler : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = ((float)PlayerPrefsHandler.GetOption("SFXVolume")) / 100f * ((float)PlayerPrefsHandler.GetOption("GeneralVolume")) / 100f;
    }
}
