using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    private GameObject dialogueBox;
    [SerializeField] private TMP_Text label, nameLabel;
    
    public bool isOpen { get; private set; }
    public DialogueData currentData { get; private set; }

    private ResponseHandler responseHandler;
    private TypewriterEffect writeEffect;

    //temp activator reference
    public IInteractable activator;

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
        currentData = data;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(data));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueData data)
    {
        for (int i = 0; i < data.dialogueText.Length; i++)
        {
            //display name
            nameLabel.text = data.dialogueText[i].speaker.ToString();
            //type text on screen
            string dialogue = data.dialogueText[i].dialogue;
            yield return RunTypingEffect(dialogue, data.dialogueText[i].unskippable);
            label.text = dialogue;

            //skip last click if there are responses
            if (i == data.dialogueText.Length - 1 && data.HasResponses) break;

            //wait 1 additional frame to make sure type effect skip doesn't advance text
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
        }

        if (data.HasResponses)
        { //dialogue not done, show responses
            responseHandler.ShowResponses(data.responsesData);
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue, bool unskippable)
    {
        writeEffect.Run(dialogue, label);

        while (writeEffect.isRunning)
        {
            yield return null;
            //allow to skip type effect
            if (!unskippable && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                writeEffect.Stop();
            }
        }
    }

    public void EndDialogue()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        //activate event
        activator?.OnEndInteract();
        activator = null;
        //update vars
        isOpen = false;
        currentData = null;
        dialogueBox.SetActive(false);
        label.text = string.Empty;
    }
}
