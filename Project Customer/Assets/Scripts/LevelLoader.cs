using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void Fade(float transitionTime = 1f)
    {
        StartCoroutine(CrossFade(transitionTime));
    }

    IEnumerator CrossFade(float transitionTime)
    {
        //start fade
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        //end fade
        transition.SetTrigger("Start");
    }
}
