using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * A class handling main menu behavior.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class MainMenu : MonoBehaviour
{
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
        // TBD
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
