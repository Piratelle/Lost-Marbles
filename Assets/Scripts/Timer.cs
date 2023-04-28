using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    public bool activeTime = false;

    public TMP_Text TimerText;

    void Start()
    {
        activeTime = true;
    }

    void Update()
    {
        if (timeLeft > 0f && activeTime == true)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timeLeft);
        }
        else
        {
            activeTime = false;
        }

    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1f;
        float minutes = Mathf.FloorToInt(currentTime/60f);
        float seconds = Mathf.FloorToInt(currentTime%60f);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        activeTime = false;
    }
}
