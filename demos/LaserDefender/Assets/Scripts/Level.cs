using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float gameOverDelay = 2f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOverActual());
    }

    IEnumerator GameOverActual()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGame()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadAfterDelay(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadAfterDelay(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadAfterDelay(int index)
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
