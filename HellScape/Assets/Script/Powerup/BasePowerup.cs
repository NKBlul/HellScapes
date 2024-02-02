using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePowerup : MonoBehaviour
{
    public PowerupSO powerupSO;

    [SerializeField] Image powerupImage;
    [SerializeField] TextMeshProUGUI powerupName;

    protected virtual void Awake()
    {
        powerupImage.sprite = powerupSO.powerupImage;
        powerupName.text = powerupSO.powerupName;
    }

    public abstract void ActivatePowerup();
}
