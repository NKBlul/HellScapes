using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    public TextMeshProUGUI masterVolume, musicVolume, sfxVolume;
    private int logVolume = 20;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume") || PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume"))
        {
            LoaddAllVolume();
        }
        else
        {
            SetAllVolume();
        }
    }

    public void SetMasterVolume()
    {       
        float volume = (float)Math.Round(masterSlider.value, 2);
        audioMixer.SetFloat("master", Mathf.Log10(volume) * logVolume);
        masterVolume.text = (volume * 100).ToString();
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = (float)Math.Round(musicSlider.value, 2);
        audioMixer.SetFloat("music", Mathf.Log10(volume) * logVolume);
        musicVolume.text = (volume * 100).ToString();
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume() 
    {
        float volume = (float)Math.Round(sfxSlider.value, 2);
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * logVolume);
        sfxVolume.text = (volume * 100).ToString();
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void SetAllVolume()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSfxVolume();
    }

    private void LoaddAllVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetAllVolume();
    }
}
