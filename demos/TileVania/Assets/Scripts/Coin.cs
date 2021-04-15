using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coinPickupSfx)
        {
            AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
        }
        FindObjectOfType<GameSession>()?.HandleScoreChange(1);
        Destroy(gameObject);
    }
}
