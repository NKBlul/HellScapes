using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource, sfxSource;
    public Sound[] music, sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(music, musics => musics.name == name);
        if (sound == null)
        {
            Debug.Log($"Music with name {name} can't be found");
            return;
        }
        musicSource.clip = sound.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfx, musics => musics.name == name);
        if (sound == null)
        {
            Debug.Log($"SFX with name {name} can't be found");
            return;
        }
        sfxSource.PlayOneShot(sound.clip);
    }
}
