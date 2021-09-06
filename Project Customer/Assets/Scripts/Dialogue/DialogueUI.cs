using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    private GameObject dialogueBox;
    [SerializeField] private TMP_Text label;

    public bool isOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypewriterEffect writeEffect;

    private void Start()
    {
        writeEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        //get obj references
        dialogueBox = transform.Find("DialogueBox").gameObject;
        //start with dialogue closed
        EndDialogue();
    }

    public void ShowDialogue(DialogueData data)
    {
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
        //update vars
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(data));
    }

    private IEnumerator StepThroughDialogue(DialogueData data)
    {
        for (int i = 0; i < data.dialogueText.Length; i++)
        {
            string dialogue = data.dialogueText[i];
            yield return RunTypingEffect(dialogue);
            label.text = dialogue;

            //skip last click if there are responses
            if (i == data.dialogueText.Length - 1 && data.HasResponses) break;

            //wait 1 additional frame to make sure type effect skip doesn't advance text
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (data.HasResponses)
        { //dialogue not done, show responses
            responseHandler.ShowResponses(data.responseData);
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        writeEffect.Run(dialogue, label);

        while (writeEffect.isRunning)
        {
            yield return null;
            //allow to skip type effect
            if (Input.GetMouseButtonDown(0))
            {
                writeEffect.Stop();
            }
        }
    }

    public void EndDialogue()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        //update vars
        isOpen = false;
        dialogueBox.SetActive(false);
        label.text = string.Empty;
    }
}
