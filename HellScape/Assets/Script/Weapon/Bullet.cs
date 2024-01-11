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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            animator.Play("Bullet_Explode");
            other.GetComponent<BaseEnemy>().TakeDamage(damage);
            rb.velocity = Vector2.zero;
        }
    }

    public void DisableCol()
    {
        col.enabled = false; 
    }

    public void ReturnBulletToPool()
    {
        col.enabled = true;
        gameObject.SetActive(false);
    }
}
