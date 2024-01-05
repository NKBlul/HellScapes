using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public TextMeshProUGUI musicVolume, sfxVolume;

    public void MusicVolume()
    {
        float convertedVolume = RoundToNearestDecimalPoint(musicSlider.value);
        AudioManager.instance.MusicVolume(convertedVolume);
        musicVolume.text = convertedVolume.ToString();
    }

    public void SfxVolume() 
    {
        float convertedVolume = RoundToNearestDecimalPoint(sfxSlider.value);
        AudioManager.instance.SFXVolume(convertedVolume);
        sfxVolume.text = convertedVolume.ToString();
    }

    private float RoundToNearestDecimalPoint(float value)
    {
        return Mathf.Round(value * 10.0f) / 10.0f;
    }
}
