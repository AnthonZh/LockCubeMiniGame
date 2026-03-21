using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    public bool GameOn;
    public UnityAPICalls APICalls;
    public AudioSource audioSource; 

    void Start()
    {
        Time.timeScale = 0.0f;
    }

    void Update()
    {
        if(!GameOn)
        {
            if(Keyboard.current.anyKey.wasPressedThisFrame)
            {
                Time.timeScale = 1.0f;
                GameOn = true;
                APICalls.StartGame();
                audioSource.Play();
            }
        }        
    }
}
