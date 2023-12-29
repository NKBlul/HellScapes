using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float elapsedTime;
    [HideInInspector] public bool isCounting;
    int minutes;
    int seconds;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        isCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = elapsedTime.ToString();
            minutes = Mathf.FloorToInt(elapsedTime / 60);
            seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void ResetTimer()
    {
        minutes = 0;
        seconds = 0;
    }
}
