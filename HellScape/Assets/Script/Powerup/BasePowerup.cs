using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BasePowerup : MonoBehaviour
{
    public PowerupSO powerupSO;

    [SerializeField] Image powerupImage;
    [SerializeField] TextMeshProUGUI powerupName;

    protected virtual void Awake()
    {
        powerupImage.sprite = powerupSO.powerupImage;
        powerupName.text = powerupSO.powerupName;
    }

    protected virtual void ActivatePowerup()
    {
        Debug.Log($"{gameObject.name} powerup activated");
        PowerupSlotsSpawner.Instance.powerupPicked = true;
        PowerupSlotsSpawner.Instance.RemoveAllPowerup();
        Spawner.Instance.NewEnemyWave(3 + Spawner.Instance.waveNum);
        PowerupSlotsSpawner.Instance.powerupSpawned = false;
    }
}
