using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int score;

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void getScore()
    {
        print(score);
    }
}
