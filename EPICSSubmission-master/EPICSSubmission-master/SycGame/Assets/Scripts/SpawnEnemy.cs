using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public ProgressBar PlayerHealth;
    public GameObject enemy;
    public global global;
    public float[] quests;
    public bool isQuest;
    public int QuestIndex;
    private int enemyCount = 0;
    private int enemyCap = 20;
    public bool Spawning;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        if (isQuest)
        {
            quests = global.AllQuests;
            if(quests.Length >= QuestIndex)
            {
                if (quests[QuestIndex] == 1f)
                {
                    enemyCap = 10;
                }
            }
        }
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (Spawning) {

        if(enemyCount >= enemyCap){
                return;
        }


        if (PlayerHealth.BarValue <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        var newEnemy= Instantiate(enemy, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        newEnemy.transform.parent = this.gameObject.transform;
        enemyCount++;
        }
        else
        {
            return;
        }
    }
}
