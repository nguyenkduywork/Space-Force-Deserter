using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Time until moving to next level")]
    [SerializeField] private float waitTime;
    [SerializeField] private ParticleSystem crashVFX;
    [SerializeField] private AudioClip PlayerExplosions;
    public Canvas sucess;
    public Canvas scoreBoard;
    
    //Turn on success screen when camera animation is over, also turn off score board
    private void Start()
    {
        Invoke("turnOnSucessCanvas",waitTime - 2f);
        Invoke("NextLevel",waitTime);
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        RespondToDebugKeys();
    }

    void turnOnSucessCanvas()
    {
        turnOffScoreBoard();
        sucess.enabled = true;
    }

    void turnOffScoreBoard()
    {
        scoreBoard.enabled = false;
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void OnTriggerEnter(Collider other)
    {
        DestroyPlayer();
    }

    

    public void DestroyPlayer()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(PlayerExplosions);
        crashVFX.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<PlayerControls>().SetLaserActive(false);
        Invoke("ReloadLevel", 1.25f);
    }
}