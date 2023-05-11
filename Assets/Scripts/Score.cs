using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Timer Timer;
    private float marbles = 0f;
    private float totalScore = 0f;
    public float marbleValue = 1000f;
    public float timeValue = 50f;

    public float[] calculate()
    {
        float marbleScore = marbles * marbleValue;
        float timeScore = Mathf.CeilToInt(Timer.timeLeft) * timeValue;
        float levelScore = marbleScore + timeScore;
        totalScore += levelScore;
        float[] scores = {marbleScore, timeScore, levelScore, totalScore};
        return scores;
    }

    public void marbleFound()
    {
        marbles++;
    }
}
