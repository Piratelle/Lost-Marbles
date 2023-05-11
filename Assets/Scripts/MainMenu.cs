using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/**
 * A class handling main menu behavior.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;

    /**
     * Called once all Scene components are Awake.
     */
    public void Start()
    {
        // load saved player audio settings
        if (PlayerPrefs.HasKey("MasterVolume")) mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        if (PlayerPrefs.HasKey("MusicVolume")) mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        if (PlayerPrefs.HasKey("SFXVolume")) mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

    /**
     * Switches into the game play scene.
     */
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelScene");
    }

    /**
     * Opens the options menu.
     */
    public void Options()
    {
        SceneManager.LoadScene("OptionsScene", LoadSceneMode.Additive);
    }

    /**
     * Switches into the credits scene.
     */
    public void Credits()
    {
        // TBD
    }

    /**
     * Terminates the program.
     */
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting!");
    }
}
