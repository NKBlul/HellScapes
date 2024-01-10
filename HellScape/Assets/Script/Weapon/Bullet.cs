using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    float damage;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D col;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
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
            other.GetComponent<BaseEnemy>().TakeDamage(damage);
            animator.Play("Bullet_Explode");
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
