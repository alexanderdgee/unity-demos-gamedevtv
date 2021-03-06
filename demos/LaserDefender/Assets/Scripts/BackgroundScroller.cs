using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.02f;
    Material material;
    Vector2 offset;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
