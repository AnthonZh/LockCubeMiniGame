using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    void Reset()
    {
        FadeOutAudio(audioSource, 5);
        Thread.Sleep(5);
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

}
