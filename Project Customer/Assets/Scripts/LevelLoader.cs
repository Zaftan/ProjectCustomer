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

    public void FadeToBlack(GameObject toActivate)
    {
        StartCoroutine(FadeToBlackCo(toActivate));
    }

    private IEnumerator FadeToBlackCo(GameObject toActivate)
    {
        label.text = "";
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        toActivate.SetActive(true);
    }
}
