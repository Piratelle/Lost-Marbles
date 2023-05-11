using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * A class handling pause menu behavior.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class PauseMenu : MonoBehaviour
{
    public static bool OPEN = false; // Because this scene is loaded additively by key input, we need to track its state.

    /**
     * Controls requests to open this Pause Menu - if keys are pressed multiple times we only want one Pause Menu.
     */
    public static void TryOpen()
    {
        if (!OPEN)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
        }
    }

    /**
     * Called once all scene components are Awake.
     */
    void Start()
    {
        OPEN = true;
    }

    /**
     * Handles scene unload.
     */
    public static void Resume()
    {
        OPEN = false;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseScene");
    }

    /**
     * Opens the options menu.
     */
    public void Options()
    {
        SceneManager.LoadScene("OptionsScene", LoadSceneMode.Additive);
    }

    /**
     * Exits back to Main Menu.
     */
    public static void Exit()
    {
        Resume();
        SceneManager.LoadScene("MainMenuScene");
    }
}
