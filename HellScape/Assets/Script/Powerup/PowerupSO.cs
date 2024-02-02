using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/Powerup")]
public class PowerupSO : ScriptableObject
{
    public Sprite powerupImage;
    public string powerupName;
}
