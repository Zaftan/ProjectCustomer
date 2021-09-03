using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    private GameObject dialogueBox;
    [SerializeField] private TMP_Text label;
    [SerializeField] private DialogueData testData;

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
        //temp test dialogue
        ShowDialogue(testData);
    }

    public void ShowDialogue(DialogueData data)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(data));
    }

    private IEnumerator StepThroughDialogue(DialogueData data)
    {
        for (int i = 0; i < data.dialogueText.Length; i++)
        {
            string dialogue = data.dialogueText[i];
            yield return writeEffect.Run(dialogue, label);

            //skip last click if there are responses
            if (i == data.dialogueText.Length - 1 && data.HasResponses) break;
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

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        label.text = string.Empty;
    }
}
