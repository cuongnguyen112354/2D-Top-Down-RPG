using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMinimapFlip : MonoBehaviour
{
    public bool flipX 
    { 
        get => flipX;
        set
        {
            spriteRenderer.flipX = value;
        }
    }

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
