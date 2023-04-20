using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class handling the behavior of an individual level.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class Level : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(0, .5f, 0);
    public GameObject player;

    /**
     * Called once all Scene components are Awake.
     */
    private void Start()
    {
        // move player onto the board
        player.transform.position = spawnPosition;
    }
}
