using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : MonoBehaviour
{
    internal SpriteRenderer spriteRenderer;

    internal float alpha = 1, velocity, targetAlpha = 1;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "P")
        {
            targetAlpha = 0.5f;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        targetAlpha = 1f;
    }
}
