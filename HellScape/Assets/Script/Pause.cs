using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isPause = false;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        isPause = true;
        Time.timeScale = 0.0f;
    }

    public void ContinueGame()
    {
        pausePanel.SetActive(false);
        isPause = false;
        Time.timeScale = 1.0f;
    }
}
