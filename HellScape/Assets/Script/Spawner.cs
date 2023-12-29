using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] Image progressionBar;
    [SerializeField] TextMeshProUGUI enemyLeftText;
    [SerializeField] TextMeshProUGUI waveText;

    public Transform spawnPoint;

    private List<GameObject> enemyPrefabs = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();

    private GameObject boss;

    private int waveNum;
    private int enemyToSpawn;
    private int numOfEnemyLeft;

    private int totalEnemyThisWave;

    // Start is called before the first frame update
    void Start()
    {
        waveNum = 0;

        enemyPrefabs.AddRange(Resources.LoadAll<GameObject>("Prefab/Enemy"));
        //check all gameobject in enemy prefabs,
        foreach(GameObject enemy in enemyPrefabs)
        {
            //if enemy name equals to orc
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
       enemyLeftText.text = numOfEnemyLeft.ToString();
       progressionBar.fillAmount = Mathf.Lerp(progressionBar.fillAmount, (float)numOfEnemyLeft / totalEnemyThisWave, Time.deltaTime * 5f);
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
        //get all gameobject from list of enemy prefabs, selecsts all that is not boss and add it to a list
        List<GameObject> minions = enemyPrefabs.Where(enemy => enemy != boss).ToList(); 
        GameObject randomEnemySpawn = minions[Random.Range(0, minions.Count)];
        return randomEnemySpawn;
    }

    private void SpawnEnemy(int numOfEnemiesToSpawn)
    {
        waveNum++;
        waveText.text = $"Wave: {waveNum}";
        totalEnemyThisWave = numOfEnemiesToSpawn;
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
