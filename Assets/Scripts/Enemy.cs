using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject deathVFX;
    [Header("This is to tidy up explosion vfx in the hierarchy")]
    [SerializeField] private Transform parent;

    [Header("Score earned when shot down")] [SerializeField]
    private int score;

    private ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessScore();
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        //move vfx objects inside a parent folder to clear up the hierarchy 
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    private void ProcessScore()
    {
        scoreBoard.IncreaseScore(score);
        scoreBoard.getScore();
    }
}