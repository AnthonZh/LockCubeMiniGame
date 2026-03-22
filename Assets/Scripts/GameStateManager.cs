using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public bool GameOn;
    public UnityAPICalls APICalls;
    public AudioSource AudioSource;
    public Camera Camera;

    bool alreadyPlayed = false;

    void Start()
    {
        Time.timeScale = 0.0f;
    }

    void Update()
    {
        if(!alreadyPlayed && !GameOn)
        {
            if(AnyInputPressed())
            {
                Time.timeScale = 1.0f;
                GameOn = true;
                APICalls.StartGame();
                AudioSource.Play();
            }
        }        
    }

    public void Reset()
    {
        GameOn = false;
        alreadyPlayed = true;
        FadeOutAudio(AudioSource, 5);
        
        Invoke(nameof(ResetHelper), 5f);
    }

    void ResetHelper()
    {
        APICalls.StopGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    bool AnyInputPressed()
    {
        foreach (var device in InputSystem.devices)
        {
            foreach (var control in device.allControls)
            {
                if (control is ButtonControl button && button.wasPressedThisFrame)
                    return true;
            }
        }
        return false;
    }
}
