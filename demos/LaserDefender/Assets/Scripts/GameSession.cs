using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        SetupSingleton();
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int delta)
    {
        score += delta;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
