using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int pooledAmount = 20;

    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject go = Instantiate(bulletPrefab);
            go.SetActive(false);
            pooledObjects.Add(go);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                // Reset bullet state before returning
                pooledObjects[i].GetComponent<SpriteRenderer>().material.color = Color.white;
                pooledObjects[i].GetComponent<Bullet>().speed = 10;
                pooledObjects[i].transform.position = Vector3.zero;
                pooledObjects[i].transform.rotation = Quaternion.identity;

                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }

        return null;
    }

    public void ReturnBulletToPool(GameObject bullet, float time = 0f)
    {
        StartCoroutine(ReturnBulletToPoolAfter(bullet, time));
    }

    private IEnumerator ReturnBulletToPoolAfter(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null && bullet.activeSelf)
        {
            bullet.SetActive(false);
        }
    }
}
