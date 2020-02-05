using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTraits : MonoBehaviour
{
    private void Update()
    {
        if (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0 && this.gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            this.gameObject.GetComponent<AudioSource>().volume = Random.Range(0.8f, 1.1f);
            this.gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.1f);
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}

