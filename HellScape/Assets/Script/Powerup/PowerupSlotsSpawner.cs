using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSlotsSpawner : MonoBehaviour
{
    public static PowerupSlotsSpawner Instance;

    private int numOfPowerup = 3;
    List<GameObject> powerups = new List<GameObject>();
    public bool powerupPicked;
    public bool powerupSpawned = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        powerups.AddRange(Resources.LoadAll<GameObject>("Prefab/Powerup"));
    }

    public void SpawnPowerUps()
    {
        for (int i = 0; i < numOfPowerup; i++) 
        {
            GameObject powerup = Instantiate(GetRandomPowerup(), transform);
            powerups.Add(powerup);
        }
        powerupSpawned = true;
    }

    public void RemoveAllPowerup()
    {
        foreach (Transform child in transform)
        {
            GameObject powerup = child.gameObject;
            powerups.Remove(powerup);
            Destroy(powerup);
        }
    }

    private GameObject GetRandomPowerup()
    {
        GameObject randomPowerup = powerups[Random.Range(0, powerups.Count)];
        return randomPowerup;
    }
}
