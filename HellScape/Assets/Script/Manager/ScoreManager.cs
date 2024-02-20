using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static TextMeshProUGUI textScore;
    public static int score = 0;
    public static int multiplier = 1;

    void Start()
    {
        textScore = GameObject.Find("scoreNum").GetComponent<TextMeshProUGUI>();
    }

    public static void UpdateScoreText(int newScore)
    {
        score += (newScore * multiplier);
        textScore.text = score.ToString();
    }
}
