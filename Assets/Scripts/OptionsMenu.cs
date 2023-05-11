using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/**
 * A class handling options menu behavior.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenTog;

    public AudioMixer mixer;

    public Slider masterSlide, musicSlide, sfxSlide;

    public TMP_Text masterLbl, musicLbl, sfxLbl;

    /**
     * Called once all Scene components are Awake.
     */
    public void Start()
    {
        // load existing settings
        fullscreenTog.isOn = Screen.fullScreen;

        // audio
        float volume = 0f;
        mixer.GetFloat("MasterVolume", out volume);
        masterSlide.value = volume;
        masterLbl.text = Mathf.RoundToInt(masterSlide.value + 80).ToString();
        mixer.GetFloat("MusicVolume", out volume);
        musicSlide.value = volume;
        musicLbl.text = Mathf.RoundToInt(musicSlide.value + 80).ToString();
        mixer.GetFloat("SFXVolume", out volume);
        sfxSlide.value = volume;
        sfxLbl.text = Mathf.RoundToInt(sfxSlide.value + 80).ToString();
    }

    /**
     * Handles scene unload.
     */
    public void Close()
    {
        SceneManager.UnloadSceneAsync("OptionsScene");
    }

    /**
     * Sets application fullscreen based on value of Fullscreen Toggle.
     */
    public void ApplyFullscreen()
    {
        Screen.fullScreen = fullscreenTog.isOn;
    }

    /**
     * Sets Master volume level based on slider value.
     */
    public void SetMasterVolume()
    {
        masterLbl.text = Mathf.RoundToInt(masterSlide.value + 80).ToString();
        mixer.SetFloat("MasterVolume", masterSlide.value);
        PlayerPrefs.SetFloat("MasterVolume", masterSlide.value);
    }

    /**
     * Sets Music volume level based on slider value.
     */
    public void SetMusicVolume()
    {
        musicLbl.text = Mathf.RoundToInt(musicSlide.value + 80).ToString();
        mixer.SetFloat("MusicVolume", musicSlide.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlide.value);
    }

    /**
     * Sets SFX volume level based on slider value.
     */
    public void SetSFXVolume()
    {
        sfxLbl.text = Mathf.RoundToInt(sfxSlide.value + 80).ToString();
        mixer.SetFloat("SFXVolume", sfxSlide.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlide.value);
    }
}
