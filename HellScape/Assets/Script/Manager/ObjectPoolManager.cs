using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int pooledAmount = 10;

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
                return pooledObjects[i];
            }
        }

        return null;
    }
}
