using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float totalTime = 300f; // Total time in seconds

    private float currentTime;
    private bool isRunning = false;
    private bool isPaused = false;

    public event Action OnTimerEnd;

    public void CustomStart()
    {
        // currentTime = totalTime;
        // UpdateTimerText();
        ResetTimer();
        StartTimer();
        OnTimerEnd += GameManager.instance.TimeOverGameOver;
    }

    private void Update()
    {
        if (isRunning && !isPaused)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                OnTimerEnd?.Invoke();
            }
            UpdateTimerText();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
