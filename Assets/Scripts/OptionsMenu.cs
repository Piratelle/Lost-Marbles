using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * A class handling options menu behavior.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class OptionsMenu : MonoBehaviour
{
    /**
     * Handles scene unload.
     */
    public void Close()
    {
        SceneManager.UnloadSceneAsync("OptionsScene");
    }
}
