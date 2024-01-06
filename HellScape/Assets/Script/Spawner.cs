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
    private int totalEnemyThisWave;
    private bool spawnNewWave;

    private Vector3 initialTextPos;
    private Vector3 initialTextScale;

    private const string BossName = "Orc";
    private const string EnemyPrefabPath = "Prefab/Enemy";
    private const int InitialWaveEnemies = 3;
    private const float TextDisplayTime = 1.5f;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        initialTextPos = UIManager.Instance.waveText.rectTransform.localPosition;
        initialTextScale = UIManager.Instance.waveText.rectTransform.localScale;

        waveNum = 0;

        enemyPrefabs.AddRange(Resources.LoadAll<GameObject>(EnemyPrefabPath));
        //check all gameobject in enemy prefabs,
        foreach(GameObject enemy in enemyPrefabs)
        {
            //if enemy name equals to orc
            if (enemy.name == BossName)
            {
                //set orc to boss
                boss = enemy;
                break;
            }
        }

        foreach (Transform child in spawnPoint.GetComponentInChildren<Transform>()) 
        {
            spawnPoints.Add(child);
        }

        NewEnemyWave(InitialWaveEnemies);
    }

    private IEnumerator WaveText(int numOfEnemiesToSpawn)
    {
        UIManager.Instance.waveText.rectTransform.localPosition = Vector3.zero;
        UIManager.Instance.waveText.rectTransform.localScale = new Vector3(2, 2, 2);
        totalEnemyThisWave = numOfEnemiesToSpawn; //update total enemy to the new number
        yield return new WaitForSeconds(TextDisplayTime);
        ResetWaveText();
        SpawnEnemy(numOfEnemiesToSpawn);
    }

    private void ResetWaveText()
    {
        UIManager.Instance.waveText.rectTransform.localPosition = initialTextPos;
        UIManager.Instance.waveText.rectTransform.localScale = initialTextScale;
    }

    // Update is called once per frame
    void Update()
    {
        numOfEnemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UIManager.Instance.enemyLeftText.text = numOfEnemyLeft.ToString();
        UIManager.Instance.progressionBar.fillAmount = Mathf.Lerp(UIManager.Instance.progressionBar.fillAmount, (float)numOfEnemyLeft / totalEnemyThisWave, Time.deltaTime * 5f);

        if (numOfEnemyLeft == 0 && !spawnNewWave) //if all enemy died
        {
            NewEnemyWave(3 + waveNum);
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
        int bossWave = 3;
        int numOfBossToSpawn = waveNum / bossWave;
        for (int i = 0; i < numOfEnemiesToSpawn; i++) 
        {
            Instantiate(RandomEnemies(), RandomSpawnPoint(), Quaternion.identity); //Spawn random enemy thats not boss, at different spawn point
        }
        if (waveNum % bossWave == 0) //every 3 wave summon a boss monster
        {
            //increase number of bullet by 1, and cap it to 3 bullets
            if (player.numOfBullet != 3)
            {
                player.numOfBullet++;
            }
            for (int i = 0; i < numOfBossToSpawn; i++) //summon extra boss every 3 wave
            {
                Instantiate(boss, RandomSpawnPoint(), Quaternion.identity); 
            }          
        }
        spawnNewWave = false;
    }

    private void NewEnemyWave(int numOfEnemiesToSpawn)
    {
        waveNum++; //Increase wave
        UIManager.Instance.waveText.text = $"Wave: {waveNum}"; //Update wave text
        spawnNewWave = true;
        StartCoroutine(WaveText(numOfEnemiesToSpawn));
    }
}
