using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer masterAudio;

    public void SetMaster(float volume)
    {
        masterAudio.SetFloat("MasterVolume", volume);
    }
    public void SetMusic(float volume)
    {
        masterAudio.SetFloat("MusicVolume", volume);
    }
    public void SetEffects(float volume)
    {
        masterAudio.SetFloat("EffectVolume", volume);
    }
    public void SetEnemies(float volume)
    {
        masterAudio.SetFloat("EnemiesVolume", volume);
    }
}
