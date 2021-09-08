using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer, audioMixer2;

    private void Start()
    {
        PlayerPrefs.GetFloat("BackgroundMusic");
        PlayerPrefs.GetFloat("SFX");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("1 - Background", volume);
        PlayerPrefs.SetFloat("BackgroundMusic", volume);
        PlayerPrefs.Save();
    }

    public void SetSFX(float volume)
    {
        audioMixer2.SetFloat("2 - SFX", volume);
        PlayerPrefs.SetFloat("SFX", volume);
        PlayerPrefs.Save();
    }
}
