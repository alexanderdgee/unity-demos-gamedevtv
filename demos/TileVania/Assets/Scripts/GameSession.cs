using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int lives = 3;
    [SerializeField] float reloadDelay = 3f;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    int score = 0;

    private void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        UpdateLives();
        UpdateScore();
    }

    private void UpdateLives()
    {
        livesText.text = lives.ToString();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void HandleScoreChange(int delta)
    {
        score += delta;
        UpdateScore();
    }

    public void HandlePlayerDeath()
    {
        lives -= 1;
        UpdateLives();
        if (lives > 0)
        {
            StartCoroutine(ReloadOnDelay());
        }
        else
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    private IEnumerator ReloadOnDelay()
    {
        yield return new WaitForSecondsRealtime(reloadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
