using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    private bool activeTime = false;
    public TMP_Text TimerText;

    void Start()
    {
        ResumeTimer();
    }

    void Update()
    {
        // count down as long as there is time left
        if (timeLeft > 0f && activeTime == true)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timeLeft);
        }
        // when out of time
        else
        {
            activeTime = false;
        }

    }

    //modifies the text and formatting of the timer
    void UpdateTimer(float currentTime)
    {
        currentTime += 1f;
        float minutes = Mathf.FloorToInt(currentTime/60f);
        float seconds = Mathf.FloorToInt(currentTime%60f);

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
