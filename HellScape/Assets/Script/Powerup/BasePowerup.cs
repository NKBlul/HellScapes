using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BasePowerup : MonoBehaviour
{
    public PowerupSO powerupSO;
    public Player player;

    [SerializeField] Image powerupImage;
    [SerializeField] TextMeshProUGUI powerupName;

    protected virtual void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        powerupImage.sprite = powerupSO.powerupImage;
        powerupName.text = powerupSO.powerupName;
    }

    public virtual void ActivatePowerup()
    {
        PowerupSlotsSpawner.Instance.powerupPicked = true;
        PowerupSlotsSpawner.Instance.RemoveAllPowerup();
        Spawner.Instance.NewEnemyWave(3 + Spawner.Instance.waveNum);
        PowerupSlotsSpawner.Instance.powerupSpawned = false;
    }
}
