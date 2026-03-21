using UnityEngine;
using TMPro;
using System.Diagnostics;
using System;
using Unity.VisualScripting;

public class TimeManager : MonoBehaviour
{
    public GameStateManager StateManager;
    public TMP_Text TimeText;
    Stopwatch stopwatch;
    void Start()
    {
        stopwatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopwatch.IsRunning && StateManager.GameOn)
        {
            stopwatch.Start();
        }
        
        if(StateManager.GameOn)
        {
            TimeSpan ts = stopwatch.Elapsed;
            TimeText.text = $"Time: {ts.Minutes:00}:{ts.Seconds:00}:{ts.Milliseconds/10:00}";
        }
    }
}
