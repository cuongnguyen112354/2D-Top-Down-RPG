using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAnimaMenu : MonoBehaviour
{
    enum ObjectType
    {
        Player,
        Slime1,
        Slime2,
        Grape,
        Ghost
    }

    [SerializeField] private ObjectType objectType;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        switch (objectType)
        {
            case ObjectType.Slime1:
                animator.SetInteger("ID", 1);
                break;
            case ObjectType.Slime2:
                animator.SetInteger("ID", 2);
                break;
            case ObjectType.Player:
                animator.SetInteger("ID", 3);
                break;
            case ObjectType.Grape:
                animator.SetInteger("ID", 4);
                break;
            case ObjectType.Ghost:
                animator.SetInteger("ID", 5);
                break;
        }
    }
}
