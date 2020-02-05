using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health = 100f;
    public float[] AllQuests = { 0 };
    public int currentLevel;
    public float[] position;

    public PlayerData(global gobal, PlayerController player)
    {
        health = global.health;
        AllQuests = global.AllQuests;
        currentLevel = global.currentLevelIndex;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
