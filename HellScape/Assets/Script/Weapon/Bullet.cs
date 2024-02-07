using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseGameObject
{
    public float speed;
    float damage;

    protected override void Start()
    {
        base.Start();

        damage = FindObjectOfType<Player>().GetDamage();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (IsAnimationFinished("Bullet_Explode"))
        {
            StartCoroutine(ReturnBulletToPool());
        }
    }

    IEnumerator ReturnBulletToPool()
    {
        spriteRenderer.material.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        yield return new WaitForSeconds(2f); // Adjust the delay if needed
        ObjectPoolManager.instance.ReturnBulletToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            speed = 0f;
            other.GetComponent<BaseEnemy>().TakeDamage(damage);
            col.enabled = false;
            animator.Play("Bullet_Explode");
        }
    }
}
