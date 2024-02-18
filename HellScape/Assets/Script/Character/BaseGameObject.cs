using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObject : MonoBehaviour
{
    [Header("References:")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {        
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
