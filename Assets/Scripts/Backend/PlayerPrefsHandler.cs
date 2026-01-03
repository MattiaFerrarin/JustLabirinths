using Unity.VisualScripting;
using UnityEngine;

public static class PlayerPrefsHandler
{
    public static void SetOption(string option, float value)
    {
        PlayerPrefs.SetFloat(option, value);
    }
    
    public static float GetOption(string option)
    {
        if (!PlayerPrefs.HasKey(option))
        {
            SetDefaultOptions();
        }
        return PlayerPrefs.GetFloat(option);
    }

    private static void SetDefaultOptions()
    {
        SetOption("GeneralVolume", 100f);
        SetOption("MusicVolume", 100f);
        SetOption("SFXVolume", 100f);
        SetOption("FOV", 60f);
        SetOption("MouseSensitivity", 400f);
    }
}