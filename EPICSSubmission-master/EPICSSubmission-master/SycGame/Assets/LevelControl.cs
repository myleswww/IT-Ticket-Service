using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public GameObject Globe;
    public string DoorIndex;
    public int index;
    public string level;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Globe.SendMessage("Transition", DoorIndex);
            SceneManager.LoadScene(index);
            SceneManager.LoadScene(level);
        }
    }
}
