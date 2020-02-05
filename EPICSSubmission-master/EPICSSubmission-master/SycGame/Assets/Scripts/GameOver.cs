using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public ProgressBar playerHealth;
    public GameObject GameOverUI;
    public PauseMenu paused;
    public global global;

    void Update()
    {
        if (playerHealth.BarValue<=0)
        {
            End();
        }
    }

    void End()
    {
        Cursor.visible = true;
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;
        paused.isPaused = true;
    }

    public void LoadMenu()
    {
        GameOverUI.SetActive(false);
        Time.timeScale = 1f;
        paused.isPaused = false;
        playerHealth.BarValue = 100;
        SceneManager.LoadScene(0);
    }

}
