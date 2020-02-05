using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioClip[] combatMusic;
    private AudioClip songClip;
    public bool nocombat;
    public bool Notcombat
    {
        get { return _notcombat; }
        set
        {
           if (value !=_notcombat)
           {
              _notcombat = value;
              GetComponent<AudioSource>().Stop();
           }
        }
    }
    private bool _notcombat;

    void Update()
    {
       if (GameObject.FindGameObjectWithTag("Enemy") != null)
       {
           nocombat = false;
       }
       else
       {
           nocombat = true;
       }
        Notcombat = nocombat;
        if (GetComponent<AudioSource>().isPlaying == false) {
            if (Notcombat)
            {
                int index = Random.Range(0, songs.Length);
                songClip = songs[index];
            }
            else
            {
                int index = Random.Range(0, combatMusic.Length);
                songClip = combatMusic[index];
            }
            GetComponent<AudioSource>().clip = songClip;
            GetComponent<AudioSource>().Play();
        }  
    }
}
