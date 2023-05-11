using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * A class handling options menu behavior.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenTog;

    /**
     * Called once all Scene components are Awake.
     */
    public void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;
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
}
