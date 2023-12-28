using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;

    private List<GameObject> enemyPrefabs = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();

    private GameObject boss;

    private int waveNum;
    private int enemyToSpawn;
    private int numOfEnemyLeft;

    // Start is called before the first frame update
    void Start()
    {
        waveNum = 0;

        enemyPrefabs.AddRange(Resources.LoadAll<GameObject>("Prefab/Enemy"));
        foreach(GameObject enemy in enemyPrefabs)
        {
            if (enemy.name == "Orc")
            {
                boss = enemy;
                break;
            }
        }

        foreach (Transform child in spawnPoint.GetComponentInChildren<Transform>()) 
        {
            spawnPoints.Add(child);
        }

        SpawnEnemy(3);
    }

    // Update is called once per frame
    void Update()
    {
       numOfEnemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
       if (numOfEnemyLeft == 0)
       {
            SpawnEnemy(3 + waveNum);
       }
    }

    Vector3 RandomSpawnPoint()
    {
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        return randomSpawn.position;
    }

    GameObject RandomEnemies()
    {
        List<GameObject> minions = enemyPrefabs.Where(enemy => enemy != boss).ToList();
        GameObject randomEnemySpawn = minions[Random.Range(0, minions.Count)];
        return randomEnemySpawn;
    }

    private void SpawnEnemy(int numOfEnemiesToSpawn)
    {
        waveNum++;
        for (int i = 0; i < numOfEnemiesToSpawn; i++) 
        {
            Instantiate(RandomEnemies(), RandomSpawnPoint(), Quaternion.identity);
        }
        if (waveNum % 3 == 0)
        {
            Instantiate(boss, RandomSpawnPoint(), Quaternion.identity);
        }  
    }
}
