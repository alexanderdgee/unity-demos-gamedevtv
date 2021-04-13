using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject winLabel;
    [SerializeField] float postWinDelay = 4f;
    [SerializeField] int attackersLeft = 0;
    [SerializeField] bool levelTimerFinished = false;

    AudioSource winSource;
    LevelLoader levelLoader;

    private void Start()
    {
        loseLabel.SetActive(false);
        winLabel.SetActive(false);
        winSource = GetComponent<AudioSource>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        foreach (var spawner in FindObjectsOfType<AttackerSpawner>())
        {
            spawner.StopSpawning();
        }
    }

    public void AttackerSpawned()
    {
        attackersLeft++;
    }

    public void AttackerKilled()
    {
        attackersLeft--;
        if (levelTimerFinished && attackersLeft <= 0)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    private IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        winSource.Play();
        yield return new WaitForSeconds(postWinDelay);
        levelLoader.LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }
}
