using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{

    private AudioSource audioSource;
    private ScoreBoard scoreBoard;
    private int playerDps;
    private MeshRenderer mesh;
    private BoxCollider thiscollider;
    GameObject parentGameObject;

    [SerializeField] private GameObject deathVFX;

    [Header("Score earned when being hit with a particle")]
    [SerializeField] private int scorePerHit = 1;

    [Header("Enemy HPs")] 
    [SerializeField] private int HP;
    
    [SerializeField] private AudioClip explosions;
    
   
    
    private void Start()
    {
        //Note to self: should not use FindObjectOfType - slow function, not advised to use multiple times (in update() etc)
        playerDps = FindObjectOfType<PlayerControls>().getDPS();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        //FindWithTag() finds the object with that tag
        parentGameObject = GameObject.FindWithTag("SpawnRuntime");
        
        //AddRigidBody();
        audioSource = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
        thiscollider = GetComponent<BoxCollider>();
    }
    
    /*
    private void AddRigidBody()
    {
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
    }
    */


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
        vfx.transform.parent = parentGameObject.transform;
        mesh.enabled = false;
        thiscollider.enabled = false;

    }
    
    //Destroy enemy GameObject
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    
    //Each time a laser hit the enemy, enemy loses HP equals to the amount of player DPS
    private void processHits()
    {
        HP -= playerDps;
        scoreBoard.IncreaseScore(scorePerHit);
    }
}