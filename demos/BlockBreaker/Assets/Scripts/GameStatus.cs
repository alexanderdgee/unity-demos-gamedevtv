using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    [SerializeField] Text scoreText;

    [SerializeField] bool autoPlay = false;
    [SerializeField] int startingLives = 3;
    [SerializeField] int autoPlayStartingLives = 10;
    [Range(0f, 10f)] [SerializeField] float timeScale = 1f;
    [SerializeField] int pointsPerBlock = 1;

    [SerializeField] int totalPoints = 0;
    [SerializeField] int livesLeft;
    [SerializeField] int ballsInPlay = 0;

    void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesLeft = startingLives;
    }

    void Update()
    {
        Time.timeScale = timeScale;
        scoreText.text = totalPoints.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool GetAutoPlay()
    {
        return autoPlay;
    }

    public void SetAutoPlay(bool willAutoplay)
    {
        autoPlay = willAutoplay;
        livesLeft = willAutoplay ? autoPlayStartingLives : startingLives;
    }

    public int GetLivesLeft()
    {
        return livesLeft;
    }

    public void AddScore(int score)
    {
        totalPoints += score;
    }

    public void AddScore()
    {
        AddScore(pointsPerBlock);
    }

    public void LaunchBall()
    {
        ++ballsInPlay;
        --livesLeft;
    }

    public void BallDeath()
    {
        --ballsInPlay;
        if (ballsInPlay + livesLeft < 1)
        {
            SceneManager.LoadScene("gameover");
        }
    }

    public void LevelComplete()
    {
        livesLeft += ballsInPlay;
        ballsInPlay = 0;
    }
}
