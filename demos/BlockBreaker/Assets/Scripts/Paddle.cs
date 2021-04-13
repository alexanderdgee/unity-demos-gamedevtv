using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] Ball ball;

    GameStatus gameStatus;
    [SerializeField] float worldWidth = 16;
    [SerializeField] float width = 2;
    [Range(0f, 10f)] [SerializeField] float spawnLockWait = 2f;
    float maxX;
    float minX;
    Vector2 targetPosition;
    List<Ball> balls = new List<Ball>();
    bool canSpawnBall = true;
    bool willReleaseSpawnLock = false;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();

        UpdateLimits();
        targetPosition = transform.position;
    }

    private void UpdateLimits()
    {
        minX = width / 2;
        maxX = worldWidth - width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition.x = Mathf.Clamp(GetTargetX(), minX, maxX);
        transform.position = targetPosition;
        SpawnBall();
    }

    private float GetTargetX()
    {
        if (gameStatus.GetAutoPlay())
        {
            if (balls.Count < 1)
            {
                return (maxX + minX) / 2;
            }
            Ball closestBall = balls[0];
            foreach (Ball ball in balls)
            {
                // TODO: closer not lowest
                if (!ball.IsLocked()
                    && ball.transform.position.y < closestBall.transform.position.y)
                {
                    closestBall = ball;
                }
            }
            return closestBall.transform.position.x;
        }
        return Input.mousePosition.x / Screen.width * worldWidth;
    }

    public void SpawnBall()
    {
        if (!canSpawnBall || gameStatus.GetLivesLeft() < 1)
        {
            return;
        }
        canSpawnBall = false;
        Ball newBall = Instantiate<Ball>(ball);
        balls.Add(newBall);
        newBall.SetPaddle(this);
    }

    IEnumerator ReleaseSpawnLock()
    {
        if (willReleaseSpawnLock)
        {
            yield break;
        }
        willReleaseSpawnLock = true;
        yield return new WaitForSeconds(spawnLockWait);
        canSpawnBall = true;
        willReleaseSpawnLock = false;
    }

    public void LaunchBall()
    {
        StartCoroutine(ReleaseSpawnLock());
    }

    internal void RemoveBall(Ball ball)
    {
        balls.Remove(ball);
    }
}
