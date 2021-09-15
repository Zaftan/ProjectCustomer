using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private List<AudioSource> sources = new List<AudioSource>();
    private int index = 0;

    private void Start()
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            sources.Add(source);
        }
    }

    public void PlayAudio(float delay)
    {
        sources[index].PlayDelayed(delay);
        index++;
        //prevent out of index
        if (index >= sources.Count) index = 0;
    }
}
