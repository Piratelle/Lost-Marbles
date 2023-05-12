using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * A class handling controls menu behavior.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class ControlsMenu : MonoBehaviour
{
    /**
     * Handles scene unload.
     */
    public void Close()
    {
        SceneManager.UnloadSceneAsync("ControlsScene");
    }
}
