using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickSound : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>();  } }



    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }
}
