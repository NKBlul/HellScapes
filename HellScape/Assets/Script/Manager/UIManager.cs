using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image progressionBar;
    public TextMeshProUGUI enemyLeftText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;

    public GameObject pause;

    private void Awake()
    {
        Instance = this;
    }
}
