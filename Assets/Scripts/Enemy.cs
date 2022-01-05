using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{

    private AudioSource audioSource;
    private ScoreBoard scoreBoard;
    private int playerDps;
    private MeshRenderer mesh;
    private BoxCollider collider;
    
    [SerializeField] private GameObject deathVFX;
    [Header("This is to tidy up explosion vfx in the hierarchy")]
    [SerializeField] private Transform parent;

    [Header("Score earned when being hit with a particle")]
    [SerializeField] private int scorePerHit = 1;

    [Header("Enemy HPs")] 
    [SerializeField] private int HP;
    
    [SerializeField] private AudioClip explosions;
    
   
    
    private void Start()
    {
        playerDps = FindObjectOfType<PlayerControls>().getDPS();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        audioSource = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }


    void OnParticleCollision(GameObject other)
    {
        processHits();
        if (HP <= 0)
        {
            if(!audioSource.isPlaying) audioSource.PlayOneShot(explosions);
            DestroyEnemySpecialEffects();
            Invoke("DestroyEnemy",1f);
        }
    }

    private void DestroyEnemySpecialEffects()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        //move vfx objects inside a parent folder to clear up the hierarchy 
        vfx.transform.parent = parent;
        mesh.enabled = false;
        collider.enabled = false;

    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void processHits()
    {
        HP -= playerDps;
        scoreBoard.IncreaseScore(scorePerHit);
    }
}