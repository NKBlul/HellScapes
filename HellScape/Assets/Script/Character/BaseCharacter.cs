using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [Header("References:")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        PlayDeadAnim();
    }

    public void PlayDeadAnim()
    {
        animator.SetTrigger("Dead");
    }

    public void FinishDeadAnim()
    {
        Destroy(gameObject);
    }

    public void SetAllEnemySpeed(float speed)
    {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        foreach (BaseEnemy enemy in enemies)
        {
            enemy.moveSpeed = speed;
        }
    }

    protected bool IsAnimationFinished(string animationName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            // Check if the normalizedTime is greater than or equal to 1
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
        }

        return false;
    }

    public float GetHP()
    {
        return currentHp;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetColor(Color32 color)
    {
        color.a = 255;
        spriteRenderer.material.color = color;
        Debug.Log("Player Color: " + spriteRenderer.color.ToHexString());
    }
}
