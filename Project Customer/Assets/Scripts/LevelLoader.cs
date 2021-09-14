using System.Collections;
using UnityEngine;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;
    [SerializeField] private TMP_Text label;

    public void Fade(string text)
    {
        StartCoroutine(CrossFade(text));
    }

    IEnumerator CrossFade(string text)
    {
        //start fade
        transition.SetTrigger("Start");
        label.text = text;
        yield return new WaitForSeconds(transitionTime);
        //end fade
        transition.SetTrigger("Start");
    }
}
