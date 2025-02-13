using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Vector2 moveDir;
    private Rigidbody2D rb;
    private KnockBack knockBack;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
    }

    void FixedUpdate()
    {
        if (knockBack.GettingKnockedBack) return;

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 position)
    {
        moveDir = position;
    }
}
