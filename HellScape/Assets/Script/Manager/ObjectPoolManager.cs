using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PooledObjectInfo
{
    public string lookupString;
    public List<GameObject> inactiveGO = new List<GameObject>();
}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;
    public List<PooledObjectInfo> objectPools = new List<PooledObjectInfo>();

    private void Awake()
    {
        instance = this;
    }

    public GameObject SpawnObject(GameObject obj, Vector3 spawnPos, Quaternion spawnRot)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.lookupString == obj.name);

        #region code explanation1
        //This is the same as the line above it
        //PooledObjectInfo pool = null;
        //foreach(PooledObjectInfo p in objectPools)
        //{
        //    if(p.lookupString == obj.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}
        #endregion

        //If the pool doesnt exist, make it
        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = obj.name};
            objectPools.Add(pool);
        }

        //Check if there any inactive objects
        GameObject spawnableObj = pool.inactiveGO.FirstOrDefault();

        #region code explanation2
        //This is the same as the line above it
        //GameObject spawnableObj = null;
        //foreach(GameObject obj in pool.inactiveGO)
        //{
        //    if (obj != null)
        //    {
        //        spawnableObj = obj;
        //        break; 
        //    }
        //}
        #endregion

        if (spawnableObj == null)
        {
            //if there are no inactive objects, create new one
            spawnableObj = Instantiate(obj, spawnPos, spawnRot);
        }
        else
        {
            //if there is inactive objecats found
            spawnableObj.transform.position = spawnPos;
            spawnableObj.transform.rotation = spawnRot;
            pool.inactiveGO.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }

    public GameObject SpawnObject(GameObject obj, Transform parent)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.lookupString == obj.name);

        #region code explanation1
        //This is the same as the line above it
        //PooledObjectInfo pool = null;
        //foreach(PooledObjectInfo p in objectPools)
        //{
        //    if(p.lookupString == obj.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}
        #endregion

        //If the pool doesnt exist, make it
        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = obj.name };
            objectPools.Add(pool);
        }

        //Check if there any inactive objects
        GameObject spawnableObj = pool.inactiveGO.FirstOrDefault();

        #region code explanation2
        //This is the same as the line above it
        //GameObject spawnableObj = null;
        //foreach(GameObject obj in pool.inactiveGO)
        //{
        //    if (obj != null)
        //    {
        //        spawnableObj = obj;
        //        break; 
        //    }
        //}
        #endregion

        if (spawnableObj == null)
        {
            //if there are no inactive objects, create new one
            spawnableObj = Instantiate(obj, parent);
        }
        else
        {
            //if there is inactive objecats found
            pool.inactiveGO.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }

    public GameObject SpawnObject(GameObject obj, Vector3 spawnPos, Quaternion spawnRot, Transform parent)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.lookupString == obj.name);

        #region code explanation1
        //This is the same as the line above it
        //PooledObjectInfo pool = null;
        //foreach(PooledObjectInfo p in objectPools)
        //{
        //    if(p.lookupString == obj.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}
        #endregion

        //If the pool doesnt exist, make it
        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = obj.name };
            objectPools.Add(pool);
        }

        //Check if there any inactive objects
        GameObject spawnableObj = pool.inactiveGO.FirstOrDefault();

        #region code explanation2
        //This is the same as the line above it
        //GameObject spawnableObj = null;
        //foreach(GameObject obj in pool.inactiveGO)
        //{
        //    if (obj != null)
        //    {
        //        spawnableObj = obj;
        //        break; 
        //    }
        //}
        #endregion

        if (spawnableObj == null)
        {
            //if there are no inactive objects, create new one
            spawnableObj = Instantiate(obj, spawnPos, spawnRot, parent);
        }
        else
        {
            //if there is inactive objecats found
            spawnableObj.transform.position = spawnPos;
            spawnableObj.transform.rotation = spawnRot;
            pool.inactiveGO.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }

    public void ReturnObjectToPool(GameObject obj, float time = 0f)
    {
        StartCoroutine(ReturnObjectToPoolAfter(obj, time));
    }

    IEnumerator ReturnObjectToPoolAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        // Check if the GameObject is still active before proceeding
        if (obj != null && obj.activeSelf)
        {
            string goName = obj.name.Substring(0, obj.name.Length - 7);

            PooledObjectInfo pool = objectPools.Find(p => p.lookupString == goName);

            if (pool == null)
            {
                Debug.LogWarning($"Gameobject is not pooled: {obj.name}");
            }
            else
            {
                obj.SetActive(false);
                pool.inactiveGO.Add(obj);
            }
        }
    }
}
