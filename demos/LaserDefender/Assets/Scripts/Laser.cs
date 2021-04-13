using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    }

    void Update()
    {
        
    }
}
