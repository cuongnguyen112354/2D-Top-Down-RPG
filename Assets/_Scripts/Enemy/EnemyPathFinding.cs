using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Vector2 moveDir;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private SpriteRenderer spriteRenderer;
    private IconMinimapFlip iconFlip;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        iconFlip = GetComponentInChildren<IconMinimapFlip>();
    }

    void FixedUpdate()
    {
        if (knockBack.GettingKnockedBack) return;

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
            iconFlip.flipX = false;
        }
        else if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
            iconFlip.flipX = true;
        }
    }

    public void MoveTo(Vector2 position)
    {
        moveDir = position;
    }

    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}
