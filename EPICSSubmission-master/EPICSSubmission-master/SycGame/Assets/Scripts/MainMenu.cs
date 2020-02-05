using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public global global;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = false;
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;

        PlayerData data = SaveSystem.LoadPlayer();

        global.health = data.health;
        global.AllQuests = data.AllQuests;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        global.PlayerPosition = position;
        global.loaded = true;

        SceneManager.LoadScene(data.currentLevel);
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
