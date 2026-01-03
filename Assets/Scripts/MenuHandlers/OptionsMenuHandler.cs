using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuHandler : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Slider _generalVolumeSlider;
    [SerializeField]
    private TMP_Text _generalVolumeValueText;
    [SerializeField]
    private Slider _musicVolumeSlider;
    [SerializeField]
    private TMP_Text _musicVolumeValueText;
    [SerializeField]
    private Slider _SFXVolumeSlider;
    [SerializeField]
    private TMP_Text _SFXVolumeValueText;
    [SerializeField]
    private Slider _FOVSlider;
    [SerializeField]
    private TMP_Text _FOVValueText;
    [SerializeField]
    private Slider _mouseSensitivitySlider;
    [SerializeField]
    private TMP_Text _mouseSensitivityValueText;
    #endregion

    void Start()
    {
        _generalVolumeSlider.value = PlayerPrefsHandler.GetOption("GeneralVolume");
        _generalVolumeValueText.text = Mathf.RoundToInt(_generalVolumeSlider.value).ToString();
        _musicVolumeSlider.value = PlayerPrefsHandler.GetOption("MusicVolume");
        _musicVolumeValueText.text = Mathf.RoundToInt(_musicVolumeSlider.value).ToString();
        _SFXVolumeSlider.value = PlayerPrefsHandler.GetOption("SFXVolume");
        _SFXVolumeValueText.text = Mathf.RoundToInt(_SFXVolumeSlider.value).ToString();
        _FOVSlider.value = PlayerPrefsHandler.GetOption("FOV");
        _FOVValueText.text = Mathf.RoundToInt(_FOVSlider.value).ToString();
        _mouseSensitivitySlider.value = PlayerPrefsHandler.GetOption("MouseSensitivity");
        _mouseSensitivityValueText.text = Mathf.RoundToInt(_mouseSensitivitySlider.value).ToString();
    }

    public void OnOptionChange(string option)
    {
        switch (option)
        {
            case "GeneralVolume":
                _generalVolumeValueText.text = Mathf.RoundToInt(_generalVolumeSlider.value).ToString();
                PlayerPrefsHandler.SetOption("GeneralVolume", _generalVolumeSlider.value);
                break;
            case "MusicVolume":
                _musicVolumeValueText.text = Mathf.RoundToInt(_musicVolumeSlider.value).ToString();
                PlayerPrefsHandler.SetOption("MusicVolume", _musicVolumeSlider.value);
                break;
            case "SFXVolume":
                _SFXVolumeValueText.text = Mathf.RoundToInt(_SFXVolumeSlider.value).ToString();
                PlayerPrefsHandler.SetOption("SFXVolume", _SFXVolumeSlider.value);
                break;
            case "FOV":
                _FOVValueText.text = Mathf.RoundToInt(_FOVSlider.value).ToString();
                PlayerPrefsHandler.SetOption("FOV", _FOVSlider.value);
                break;
            case "MouseSensitivity":
                _mouseSensitivityValueText.text = Mathf.RoundToInt(_mouseSensitivitySlider.value).ToString();
                PlayerPrefsHandler.SetOption("MouseSensitivity", _mouseSensitivitySlider.value);
                break;
        }
    }
}
