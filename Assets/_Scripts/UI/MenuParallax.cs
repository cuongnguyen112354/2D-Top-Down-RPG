using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float offsetMultiplier = 0.5f;
    [SerializeField] private float smoothTime = 0.3f;

    private Vector2 startPos;
    private Vector3 velocity;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, startPos + (Vector2.one - offset) * offsetMultiplier, ref velocity, smoothTime);
    }
}
