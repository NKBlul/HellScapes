using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    float damage;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D col;
    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        damage = FindObjectOfType<Player>().GetDamage();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (IsAnimationFinished("Bullet_Explode"))
        {
            //ObjectPoolManager.instance.ReturnBulletToPool(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BaseEnemy>().TakeDamage(damage);
            animator.Play("Bullet_Explode");
            col.enabled = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void DisableCol()
    {
        col.enabled = false; 
    }

    bool IsAnimationFinished(string animationName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            // Check if the normalizedTime is greater than or equal to 1
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
        }

        return false;
    }
}
