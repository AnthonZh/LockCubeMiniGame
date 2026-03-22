using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class UnityAPICalls : MonoBehaviour
{
    private readonly string StartEndpoint = "http://127.0.0.1:5001/game/start";
    private readonly string StopEndpoint = "http://127.0.0.1:5001/game/stop";

    public void StartGame()
    {
        StartCoroutine(GetRequest(StartEndpoint));
    }

    public void StopGame()
    {
        StartCoroutine(GetRequest(StopEndpoint));
    }

    private IEnumerator GetRequest(string uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}
