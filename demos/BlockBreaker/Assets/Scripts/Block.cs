using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip destroyedClip;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] GameObject sparklesVfx;

    Level level;
    SpriteRenderer spriteRenderer;
    [SerializeField] Color32? color = null;

    int spriteIndex = -1;
    [SerializeField] int maxDurability = 1;
    [SerializeField] int durability;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer && color != null)
        {
            spriteRenderer.color = (Color32)color;
        }

        durability = maxDurability;
        if (IsBreakable())
        {
            level.BlockCreated();
        }
    }

    public void SetDurability(int newDurability)
    {
        durability = newDurability;
        if (newDurability > maxDurability)
        {
            maxDurability = newDurability;
        }
    }

    public void SetColor(Color32 newColor)
    {
        color = newColor;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsBreakable())
        {
            DamageBlock();
        }
    }

    private bool IsBreakable()
    {
        return tag == "Breakable";
    }

    private void DamageBlock()
    {
        --durability;
        if (durability < 1)
        {
            DestroyBlock();
        }
        else if (hitSprites.Length > 0)
        {
            var newSpriteIndex = (int)System.Math.Floor(
                (1 - 1f * durability / maxDurability) * hitSprites.Length);
            if (newSpriteIndex != spriteIndex)
            {
                spriteIndex = newSpriteIndex;
                UpdateSprite(hitSprites[spriteIndex]);
            }
        }
    }

    private void UpdateSprite(Sprite sprite)
    {
        if (spriteRenderer)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(destroyedClip, transform.position);
        level.BlockDestroyed();
        var sparkles = Instantiate(sparklesVfx, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
        Destroy(gameObject);
    }
}
