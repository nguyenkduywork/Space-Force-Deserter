using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Time until moving to next level")]
    [SerializeField] private float waitTime;

    [SerializeField] private ParticleSystem crashVFX;

    public Canvas sucess;
    public Canvas scoreBoard;
    
    //Turn on success screen when camera animation is over, also turn off score board
    private void Start()
    {
        Invoke("turnOnSucessCanvas",waitTime - 2f);
        Invoke("NextLevel",waitTime);
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
            //SceneManager.LoadScene("Menu");
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
        Debug.Log($"{name} TRIGGERED BY {other.gameObject.name}");
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<PlayerControls>().SetLaserActive(false);
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }
}