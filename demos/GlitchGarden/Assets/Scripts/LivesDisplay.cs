using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] int baseLives = 10;

    int lives = 5;
    Text livesText;
    LevelController levelController;

    void Start()
    {
        lives = baseLives;
        var difficulty = PlayerPrefsController.GetDifficulty();
        if (difficulty > 2) { lives = 1; }
        else if (difficulty > 1) { lives = lives / 3; }
        else if (difficulty > 0) { lives = lives / 2; }
        levelController = FindObjectOfType<LevelController>();
        livesText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        livesText.text = lives.ToString();
    }

    public void AddLives(int amount)
    {
        lives += amount;
        UpdateDisplay();
    }

    public void TakeLife()
    {
        lives -= 1;
        UpdateDisplay();
        if (lives < 1)
        {
            levelController.HandleLoseCondition();
        }
    }
}
