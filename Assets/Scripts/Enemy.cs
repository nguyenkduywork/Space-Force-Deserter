using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject deathVFX;
    [Header("This is to tidy up explosion vfx in the hierarchy")]
    [SerializeField] private Transform parent;

    [Header("Score earned when being hit with a particle")]
    [SerializeField] private int scorePerHit = 1;

    [Header("Enemy HPs")] 
    [SerializeField] private int HP;

    private ScoreBoard scoreBoard;
    private int playerDps;
    private void Start()
    {
        playerDps = FindObjectOfType<PlayerControls>().getDPS();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }


    void OnParticleCollision(GameObject other)
    {
        processHits();
        if(HP <= 0) DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        //move vfx objects inside a parent folder to clear up the hierarchy 
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    private void processHits()
    {
        HP -= playerDps;
        scoreBoard.IncreaseScore(scorePerHit);
    }
}