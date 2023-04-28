using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class handling the behavior of an individual level, tied to the Level's Plane.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class Level : MonoBehaviour
{
    public GameObject player;
    public GameObject wallPrefab;

    MazeData maze;


    /**
     * Called once all Scene components are Awake.
     */
    private void Start()
    {
        // build/customize level floor
        GameObject lvl = this.gameObject;
        maze = new MazeData(11, 11); // use default values for now, dynamic at some point?
        lvl.transform.localScale = new Vector3(maze.Width(), maze.Height(), maze.Depth());

        // populate level walls
        foreach (MazeData.Wall wall in maze.walls)
        {
            GameObject wallGO = Instantiate<GameObject>(wallPrefab, lvl.transform);
            maze.BuildWall(wallGO, wall);
        }

        // move player onto the board
        player.transform.position = maze.SpawnPos();
    }

    
}
