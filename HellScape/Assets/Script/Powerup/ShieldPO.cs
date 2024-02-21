using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPO : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BaseEnemy>().TakeDamage(other.GetComponent<BaseEnemy>().GetHP());
            ObjectPoolManager.instance.ReturnObjectToPool(gameObject);
        }
    }
}
