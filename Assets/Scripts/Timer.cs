using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float TIME_LEFT;
    private bool activeTime = false;
    public TMP_Text TimerText;
    public AudioSource lossSound;

    void Start()
    {
        ResumeTimer();
    }

    public static void Initialize(float timeLeft)
    {
        TIME_LEFT = timeLeft;
    }

    void Update()
    {
        // count down as long as there is time left
        if (activeTime)
        {
            if (TIME_LEFT > 0f)
            {
                TIME_LEFT -= Time.deltaTime;
                UpdateTimer(TIME_LEFT);
            }
            // when out of time
            else
            {
                lossSound.Play();
                StopTimer();
                Level.GameOver();
            }
        }
    }

    //modifies the text and formatting of the timer
    void UpdateTimer(float currentTime)
    {
        currentTime += 1f;
        int minutes = (int) currentTime / 60;
        int seconds = (int) currentTime % 60;

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    //stop the timer, for pause menus, level ends and the like
    public void StopTimer()
    {
        activeTime = false;
    }

    public void ResumeTimer()
    {
        activeTime = true;
    }
}
