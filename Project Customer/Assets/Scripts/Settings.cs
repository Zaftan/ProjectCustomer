using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer, audioMixer2;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("1 - Background", volume);
    }

    public void SetSFX(float volume)
    {
        audioMixer2.SetFloat("2 - SFX", volume);
    }
}
