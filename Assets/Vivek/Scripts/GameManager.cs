using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxCoinCount { get; private set; }
    [HideInInspector] public int coinCount;
    public Timer timer;
    public ScoreKeeper scoreKeeper;
    public List<coin> allCollectableCoins;
    public bool isGameOver { get; private set; }
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isGameOver = false;
        gameOverPanel.SetActive(false);
        maxCoinCount = allCollectableCoins.Count;
        scoreKeeper.CustomStart();
        timer.CustomStart();
    }

    public void GameOverHappened()
    {
        isGameOver = true;
        gameOverText.text = "All coins collected. Good job.";
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void TimeOverGameOver()
    {
        isGameOver = true;
        gameOverText.text = "Time Over. Try again.";
        gameOverPanel.SetActive(true);
    }
}
