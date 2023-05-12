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
    private static float exitDelay = 5f;
    private static string nextScene;
    private static Level CURR_LVL;

    public GameObject player;
    public GameObject goal;
    public GameObject wallPrefab;

    GameObject lvl;
    MazeData maze;

    /**
     * Reset level counter and difficulty.
     */
    public static void Reset()
    {
        LEVEL = 0;
        Score.Reset();
    }

    /**
     * Called once all Scene components are Awake.
     */
    private void Start()
    {
        // increment level
        CURR_LVL = this;
        LEVEL += 1;
        Timer.Initialize(30f * LEVEL);

        // build/customize level floor
        lvl = this.gameObject;
        maze = new MazeData(10 + LEVEL, 10 + LEVEL);
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
    }

    /**
     * Handles end-of-game behavior.
     */
    public static void GameOver()
    {
        nextScene = "GameOverScene";
        if (Timer.TIME_LEFT > 0)
        {
            // Continuous Play settings
            bool continuePlay = false;
            if (PlayerPrefs.HasKey("Continue")) continuePlay = (PlayerPrefs.GetInt("Continue") > 0);

            if (continuePlay) {
                //SceneManager.LoadScene("LevelScene");
                nextScene = "LevelScene";
                //return;
            }
        }
        //SceneManager.LoadScene("GameOverScene");
        CURR_LVL.Invoke("ExitScene", exitDelay);
    }

    /**
     * Loads next scene.
     */
    void ExitScene()
    {
        Score playerScore = player.GetComponentInChildren<Score>();
        playerScore.calculate();
        SceneManager.LoadScene(nextScene);
    }
}
