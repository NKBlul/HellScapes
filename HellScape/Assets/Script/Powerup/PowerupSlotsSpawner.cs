using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSlotsSpawner : MonoBehaviour
{
    public static PowerupSlotsSpawner Instance;

    private int numOfPowerup = 3;
    List<GameObject> powerups = new List<GameObject>();
    public bool powerupPicked;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        powerups.AddRange(Resources.LoadAll<GameObject>("Prefab/Powerup"));

        SpawnPowerUps();
    }

    public void SpawnPowerUps()
    {
        for (int i = 0; i < numOfPowerup; i++) 
        {
            GameObject powerup = Instantiate(GetRandomPowerup(), transform);
            powerups.Add(powerup);
        }
    }

    public void RemoveAllPowerup()
    {
        powerups.Clear();
        foreach (Transform child in transform)
        {
            GameObject powerup = child.gameObject;
            Destroy(powerup);
        }
    }

    private GameObject GetRandomPowerup()
    {
        GameObject randomPowerup = powerups[Random.Range(0, powerups.Count)];
        return randomPowerup;
    }
}
