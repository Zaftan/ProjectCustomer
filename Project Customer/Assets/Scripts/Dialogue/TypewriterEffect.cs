using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float delay;

    public Coroutine Run(string msg, TMP_Text label)
    {
        return StartCoroutine(TypeText(msg, label));
    }

    private IEnumerator TypeText(string msg, TMP_Text label)
    {
        //reset label
        label.text = string.Empty;
        //type character 1 at a time
        for (int i = 1; i <= msg.Length; i++)
        {
            label.text = msg.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
        label.text = msg;
    }
}
