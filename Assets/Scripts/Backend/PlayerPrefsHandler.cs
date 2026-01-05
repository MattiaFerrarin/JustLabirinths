using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;

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
        SetOption("MusicVolume", 80f);
        SetOption("SFXVolume", 100f);
        SetOption("FOV", 60f);
        SetOption("MouseSensitivity", 400f);
    }

    public static void SaveLevels()
    {
        string json = JsonConvert.SerializeObject(GameManager.Levels);
        PlayerPrefs.SetString($"Levels", json);
    }

    public static void LoadLevels()
    {
        if (PlayerPrefs.HasKey($"Levels"))
        {
            string json = PlayerPrefs.GetString($"Levels");
            GameManager.Levels = JsonConvert.DeserializeObject<Dictionary<int,LevelStatus>>(json);
        }
    }
}