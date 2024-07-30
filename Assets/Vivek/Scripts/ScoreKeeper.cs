using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private static int score;

    public static event Action OnScoreIncrease;
    public static event Action<int> OnCoinCollect;


    public void CustomStart()
    {
        score = 0;
        IncreaseScore(score);
        OnScoreIncrease += UpdateScoreText;
        OnScoreIncrease?.Invoke();
        OnCoinCollect += CheckForTotalCoins;
        OnCoinCollect?.Invoke(score);
    }

    public static void IncreaseScore(int amount)
    {
        score += amount;
        OnScoreIncrease?.Invoke();

        OnCoinCollect?.Invoke(amount);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void CheckForTotalCoins(int amount)
    {
        GameManager.instance.coinCount += amount;
        if (GameManager.instance.coinCount == GameManager.instance.maxCoinCount)
        {
            GameManager.instance.timer.PauseTimer();
            // Handle all coins collected event
            Debug.Log("All coins collected!");
            GameManager.instance.GameOverHappened();
        }
    }
}
