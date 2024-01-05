using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public TextMeshProUGUI musicVolume, sfxVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFXVolume();
        }
    }

    public void MusicVolume()
    {
        float convertedMusicVolume = RoundToNearestDecimalPoint(musicSlider.value);
        AudioManager.instance.MusicVolume(convertedMusicVolume);
        musicVolume.text = convertedMusicVolume.ToString();
        PlayerPrefs.SetFloat("musicVolume", convertedMusicVolume);
    }

    public void SfxVolume() 
    {
        float convertedSFXVolume = RoundToNearestDecimalPoint(sfxSlider.value);
        AudioManager.instance.SFXVolume(convertedSFXVolume);
        sfxVolume.text = convertedSFXVolume.ToString();
        PlayerPrefs.SetFloat("sfxVolume", convertedSFXVolume);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private float RoundToNearestDecimalPoint(float value)
    {
        return Mathf.Round(value * 10.0f) / 10.0f;
    }
}
