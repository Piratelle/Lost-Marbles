using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float marbles = 0f;
    //private float totalScore = 0f;
    public float marbleValue = 1000f;
    public float timeValue = 50f;

    public static float LVL_SCORE = 0f;
    public static float TOTAL_SCORE = 0f;

    public static void Reset()
    {
        TOTAL_SCORE = 0f;
        LVL_SCORE = 0f;
    }

    public float[] calculate()
    {
        float marbleScore = marbles * marbleValue;
        float timeScore = Mathf.CeilToInt(Timer.TIME_LEFT) * timeValue;
        //float levelScore = marbleScore + timeScore;
        LVL_SCORE = marbleScore + timeScore;
        //totalScore += levelScore;
        TOTAL_SCORE += LVL_SCORE;
        //float[] scores = {marbleScore, timeScore, levelScore, totalScore};
        float[] scores = { marbleScore, timeScore, LVL_SCORE, TOTAL_SCORE };
        return scores;
    }

    public void marbleFound()
    {
        marbles++;
    }
}
