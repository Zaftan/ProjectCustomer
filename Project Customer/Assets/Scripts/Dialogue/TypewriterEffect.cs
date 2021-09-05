using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Delay between letters typed in seconds")]
    [SerializeField] private float delay;

    public bool isRunning { get; private set; }

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        { new HashSet<char>(){ '.', '!', '?' }, 0.5f},
        { new HashSet<char>(){ ',', ';', ':' }, 0.2f}
    };

    private Coroutine typingCoroutine;

    public void Run(string msg, TMP_Text label)
    {
        typingCoroutine = StartCoroutine(TypeText(msg, label));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    private IEnumerator TypeText(string msg, TMP_Text label)
    {
        isRunning = true;
        //reset label
        label.text = string.Empty;
        //type one character at a time
        for (int i = 0; i < msg.Length; i++)
        {
            label.text = msg.Substring(0, i + 1);

            bool isLast = i == msg.Length - 1;
            if (IsPunctuation(msg[i], out float waitTime) && !isLast && !IsPunctuation(msg[i + 1], out _))
            {
                yield return new WaitForSeconds(waitTime);
            }
            //generic pause
            yield return new WaitForSeconds(delay);
        }
        //end
        isRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (KeyValuePair<HashSet<char>, float> puncCategory in punctuations)
        {
            if (puncCategory.Key.Contains(character))
            {
                waitTime = puncCategory.Value;
                return true;
            }
        }
        waitTime = default;
        return false;
    }
}
