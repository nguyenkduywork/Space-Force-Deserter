using System;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score;
    
    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
    
    
}
