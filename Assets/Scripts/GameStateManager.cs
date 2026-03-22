using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public bool GameOn;
    public UnityAPICalls APICalls;
    public AudioSource AudioSource;
    public AudioSource SFX;
    public AudioClip WinSFX;
    public AudioClip LoseSFX;
    public Camera Camera;
    public GameObject WinText;
    public GameObject GameOverText;
    public GameObject StartText;

    bool alreadyPlayed = false;

    void Start()
    {
        WinText.SetActive(false);
        GameOverText.SetActive(false);
        Time.timeScale = 0.0f;
    }

    void Update()
    {
        if(!alreadyPlayed && !GameOn)
        {
            if(AnyInputPressed())
            {
                StartText.SetActive(false);
                Time.timeScale = 1.0f;
                GameOn = true;
                APICalls.StartGame();
                AudioSource.Play();
            }
        }        
    }

    public void Reset()
    {
        foreach (var rb in FindObjectsOfType<Rigidbody2D>())
        {
            rb.simulated = false;
        }

        foreach (var saw in FindObjectsOfType<SawScript>())
        {
            saw.moving = false;
        }

        GameOn = false;
        alreadyPlayed = true;
        StartCoroutine(FadeOutAudio(AudioSource, 5f));
        SFX.PlayOneShot(LoseSFX);
        GameOverText.SetActive(true);
        Invoke(nameof(SendEndHelper), 3f);
        Invoke(nameof(ResetHelper), 5f);
    }

    public void Win()
    {
        foreach (var rb in FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None))
        {
            rb.simulated = false;
        }

        foreach (var saw in FindObjectsByType<SawScript>(FindObjectsSortMode.None))
        {
            saw.moving = false;
        }

        GameOn = false;
        alreadyPlayed = true;
        StartCoroutine(FadeOutAudio(AudioSource, 5f));
        SFX.PlayOneShot(WinSFX);
        WinText.SetActive(true);
        Invoke(nameof(SendEndHelper), 3f);
        Invoke(nameof(ResetHelper), 5f);
    }

    void ResetHelper()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SendEndHelper()
    {
        APICalls.StopGame();
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
