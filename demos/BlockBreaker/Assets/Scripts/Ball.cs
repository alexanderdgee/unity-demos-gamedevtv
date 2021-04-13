using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float yOffsetFromPaddle = 0.5f;
    [SerializeField] float xLaunchSpeed = 2f;
    [SerializeField] float yLaunchSpeed = 10f;
    [SerializeField] float randomFactor = 0.1f;
    [SerializeField] AudioClip[] collisionSounds;

    bool locked = true;
    Paddle paddle = null;

    AudioSource audioSource;
    new Collider2D collider;
    new Rigidbody2D rigidbody;
    GameStatus gameStatus;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        gameStatus = FindObjectOfType<GameStatus>();

        collider.enabled = false;
        if (paddle == null)
        {
            SetPaddle(FindObjectOfType<Paddle>());
        }
    }

    void Update()
    {
        if (locked)
        {
            RefreshLockedPosition();
            if (Input.GetMouseButtonDown(0))
            {
                Launch();
            }
        }
    }

    public void SetPaddle(Paddle gamePaddle)
    {
        paddle = gamePaddle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!locked)
        {
            IncreaseSpeed();
            if (collisionSounds.Length > 0)
            {
                AudioClip clip = collisionSounds[
                    Random.Range(0, collisionSounds.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }

    public bool IsLocked()
    {
        return locked;
    }

    private void IncreaseSpeed()
    {
        var deltaVMinX = rigidbody.velocity.x > 0 ? 0 : randomFactor * -1;
        var deltaVMaxX = rigidbody.velocity.x < 0 ? 0 : randomFactor * 1;
        var deltaVMinY = rigidbody.velocity.y > 0 ? 0 : randomFactor * -1;
        var deltaVMaxY = rigidbody.velocity.y < 0 ? 0 : randomFactor * 1;
        var deltaV = new Vector2(
            Random.Range(deltaVMinX, deltaVMaxX),
            Random.Range(deltaVMinY, deltaVMaxY));
        rigidbody.velocity += deltaV;
    }

    private void RefreshLockedPosition()
    {
        transform.position = new Vector2(
            paddle.transform.position.x,
            paddle.transform.position.y + yOffsetFromPaddle);
    }

    private void Launch()
    {
        rigidbody.velocity = new Vector2(xLaunchSpeed, yLaunchSpeed);
        collider.enabled = true;
        paddle.LaunchBall();
        gameStatus.LaunchBall();
        locked = false;
    }

    public void Die()
    {
        paddle.RemoveBall(this);
        gameStatus.BallDeath();
        Destroy(gameObject);
    }
}
