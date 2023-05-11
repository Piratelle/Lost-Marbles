using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * A class handling the behavior of an individual level, tied to the Level's Plane.
 * 
 * @author Erin Ratelle
 * @author Ryan Smith
 */
public class Level : MonoBehaviour
{
    public static int LEVEL = 0;
    public static float TIME_LEFT = 0f;

    public GameObject player;
    public GameObject goal;
    public GameObject wallPrefab;

    public float changeTiltChance = 0.05f;
    public float changeTiltDirChance = 0.1f;
    public float tiltSpeed = 5f;

    GameObject lvl;
    MazeData maze;
    bool randomTilt = false;
    bool tiltPos = true;
    float maxTilt = 45f;

    /**
     * Reset level counter and difficulty.
     */
    public static void Reset()
    {
        LEVEL = 0;
    }

    /**
     * Called once all Scene components are Awake.
     */
    private void Start()
    {
        // increment level
        LEVEL += 1;

        // check relevant player prefs
        if (PlayerPrefs.HasKey("TiltRand")) randomTilt = (PlayerPrefs.GetInt("TiltRand") > 0);

        // build/customize level floor
        lvl = this.gameObject;
        maze = new MazeData(10 + LEVEL, 10 + LEVEL); // use default values for now, dynamic at some point?
        lvl.transform.localScale = new Vector3(maze.Width(), maze.Height(), maze.Depth());

        // populate level walls
        foreach (MazeData.Wall wall in maze.walls)
        {
            GameObject wallGO = Instantiate<GameObject>(wallPrefab, lvl.transform);
            maze.BuildWall(wallGO, wall);
        }

        // place goal
        Vector3 goalFinal = new Vector3(maze.GoalPos().x, -.075f, maze.GoalPos().z);
        goal.transform.position = goalFinal;
        

        // move player onto the board
        player.transform.position = maze.SpawnPos();
    }

    /**
     * Frame update. 
     * Handles applicable user input.
     */
    void Update()
    {
        // handle pausing
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseMenu.TryOpen();
        }

        // handle tilting
        bool tilt = false;
        if (!randomTilt)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                tiltPos = false;
                tilt = true;
            }
            if (Input.GetKey(KeyCode.E))
            {
                tiltPos = true;
                tilt = true;
            }
        } else
        {
            if (Random.value < changeTiltDirChance) tiltPos = !tiltPos;
            tilt = (Random.value < changeTiltChance);
        }
        if ((tiltPos && lvl.transform.rotation.eulerAngles.x >= maxTilt) || (!tiltPos && lvl.transform.rotation.eulerAngles.x <= 360 - maxTilt && lvl.transform.rotation.eulerAngles.x > 180)) tilt = false;
        if (tilt) lvl.transform.Rotate((tiltPos ? 1 : -1) * tiltSpeed * Time.deltaTime, 0f, 0f);
    }

    /**
     * Handles end-of-game behavior.
     */
    public void GameOver()
    {
        if (TIME_LEFT > 0)
        {
            // Continuous Play settings
            bool continuePlay = false;
            if (PlayerPrefs.HasKey("Continue")) continuePlay = (PlayerPrefs.GetInt("Continue") > 0);

            if (continuePlay) {
                SceneManager.LoadScene("LevelScene");
                return;
            }
        }
        SceneManager.LoadScene("GameOverScene");
    }
}
