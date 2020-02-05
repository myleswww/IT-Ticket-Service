using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class global : MonoBehaviour
{
    static public string doorID;
    static public float health = 100f;
    static public float[] AllQuests = {0};
    static public int currentLevelIndex;
    static public Vector3 PlayerPosition;
    static public bool loaded = false;
    public bool isLevelStart = true;
    public GameObject[] spawnPoints;
    public Transform Player;
    public ProgressBar PlayerHealth;


private void Start()
    {
        if (isLevelStart)
        {
            levelStart();
        }
    }


   public void Restart()
    {
        doorID = null;
        health = 100f;
        for(int i= 0; i > AllQuests.Length; i++)
        {
            AllQuests.SetValue(0, i);
        }
    }



    void levelStart()
    {
        PlayerHealth.BarValue = health;
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].GetComponent<LevelControl>().DoorIndex == doorID)
            {
                Player.position = spawnPoints[i].transform.position;
            }
        }
        if (loaded)
        {
            Player.position = PlayerPosition;
            loaded = false;
        }
    }

    void Transition(string doorIndex)
    {
        doorID = doorIndex;
    }

    void Update()
    {
        if (isLevelStart)
        {
            health = PlayerHealth.BarValue;
        }
    }
}
