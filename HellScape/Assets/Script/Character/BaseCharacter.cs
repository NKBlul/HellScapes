using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [Header("References:")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;
    [HideInInspector] public Animator animator;

    [Header("Stats:")]
    protected float maxHp;
    protected float currentHp;
    protected float damage;
    protected float moveSpeed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log(currentHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //animator.SetBool("Dead", true);
        animator.SetTrigger("Dead");
    }

    public float GetHP()
    {
        return currentHp;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void FinishDeadAnim()
    {
        Destroy(gameObject);
    }
}
