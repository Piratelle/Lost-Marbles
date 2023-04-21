using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds, sfxLoopSounds;
    public AudioSource musicSource, sfxSource, sfxLoopSource;

private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
    }
    else
    {
        Destroy(gameObject);
    }
}

private void Start()
{
}


public void PlayMusic(string name, bool looping = false)
{
    Sound s = Array.Find(musicSounds, x => x.name == name);

    if (s == null) 
    {
        Debug.Log("Music '" + name + "' not found");
    }
    else
    {
        musicSource.clip = s.clip;
        musicSource.Play();
        if (looping){
            musicSource.loop = true;
        }
        else{
            musicSource.loop = false;
        }
    }
}

public void PlaySFX(string name, float vol = .15f)
{
    Sound s = Array.Find(sfxSounds, x => x.name == name);

    if (s == null) 
    {
        Debug.Log("SFX '" + name + "' not found");
    }
    else
    {
        sfxSource.PlayOneShot(s.clip, vol);
    }
}

public void PlayLoopSFX(string name, float vol = .15f)
{
    Sound s = Array.Find(sfxLoopSounds, x => x.name == name);

    if (s == null) 
    {
        Debug.Log("SFX '" + name + "' not found");
    }
    else
    {
        Debug.Log("SFXLoop Playing: " + name);
        sfxLoopSource.clip = s.clip;
        sfxLoopSource.Play();
    }
}

public void PauseLoopSFX(string name)
{
    Sound s = Array.Find(sfxLoopSounds, x => x.name == name);

    if (s == null) 
    {
        Debug.Log("SFX '" + name + "' not found");
    }
    else
    {
        Debug.Log("SFXLoop Pausing: " + name);
        sfxLoopSource.Pause();
    }
}

public void StopLoopSFX(string name)
{
    Sound s = Array.Find(sfxLoopSounds, x => x.name == name);

    if (s == null) 
    {
        Debug.Log("SFX '" + name + "' not found");
    }
    else
    {
        sfxLoopSource.Stop();
    }
}

}