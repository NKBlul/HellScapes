using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseGameObject
{
    Player player;
    public float speed;
    float damage;

    protected override void Start()
    {
        base.Start();
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
            ObjectPoolManager.instance.ReturnObjectToPool(gameObject);
        }
    }

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        damage = player.GetDamage();
    }
}
