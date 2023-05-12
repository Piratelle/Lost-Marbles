using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    /**
     * Updates values based on Level.
     */
    void Awake()
    {
        // show time remaining
        GameObject timeGO = GameObject.Find("Time Value");
        TMP_Text timeText = timeGO.GetComponent<TMP_Text>();
        int timeRem = (int)Timer.TIME_LEFT;
        int minutes = timeRem / 60;
        int seconds = timeRem % 60;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // show levels
        GameObject levelGO = GameObject.Find("Level Value");
        TMP_Text levelText = levelGO.GetComponent<TMP_Text>();
        int level = Level.LEVEL;
        levelText.text = level.ToString("#,0");

        // show level score
        GameObject scoreGO = GameObject.Find("Score Value");
        TMP_Text scoreText = scoreGO.GetComponent<TMP_Text>();
        int score = Mathf.CeilToInt(Score.LVL_SCORE);
        scoreText.text = score.ToString("#,0");

        // show total score
        GameObject totalGO = GameObject.Find("Total Value");
        TMP_Text totalText = totalGO.GetComponent<TMP_Text>();
        int total = Mathf.CeilToInt(Score.TOTAL_SCORE);
        totalText.text = total.ToString("#,0");

        // update next level button
        GameObject nextBtnGO = GameObject.Find("Next Button");
        TMP_Text nextText = nextBtnGO.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        if (timeRem > 0)
        {
            nextText.text = "Next Level";
        } else
        {
            nextText.text = "Play Again";
            Level.Reset(); // reset level progress once player loses
        }
    }

    /**
     * Starts a new game for the player.
     */
    public void PlayAgain()
    {
        SceneManager.LoadScene("LevelScene");
    }

    /**
     * Exits back to Main Menu.
     */
    public void Exit()
    {
        Level.Reset(); // reset level progress once player stops playing
        SceneManager.LoadScene("MainMenuScene");
    }

    
}
