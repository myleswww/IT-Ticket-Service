using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public global global;
    public PlayerController player;
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    private GameObject saveGameUI;
    public Soundtrack enemyDetector;
    private void Start()
    {
        isPaused = false;
        saveGameUI = pauseMenuUI.transform.GetChild(1).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        if (!enemyDetector.Notcombat)
        {
            saveGameUI.SetActive(false);
        }
            Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        if (enemyDetector.Notcombat)
        {
            SaveSystem.SavePlayer(global, player);
        }
    }

}
