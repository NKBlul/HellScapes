using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCharacter : BaseGameObject
{
    [Header("Stats:")]
    protected float maxHp;
    protected float currentHp;
    protected float damage;
    protected float moveSpeed;

    [HideInInspector] public Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
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
}
