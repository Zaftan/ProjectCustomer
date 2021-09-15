using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private DialogueMessage[] dialogue;
    [SerializeField] private Response[] responses;

    public DialogueMessage[] dialogueText => dialogue;
    //response getters
    public Response[] responsesData => responses;
    public bool HasResponses => responses != null && responses.Length > 0;

    [System.Serializable]
    public enum Expression
    {
        Happy,
        Neutral,
        Sad,
        Guilt,
        Depressed
    }
    [System.Serializable]
    public enum Speaker
    {
        Investigator,
        Megan,
        Fred,
        Bob,
        Orlando,
        Police
    }

    //data struct
    [System.Serializable]
    public struct DialogueMessage
    {
        [SerializeField] [TextArea] public string dialogue;
        [SerializeField] public bool unskippable;
        [SerializeField] public Expression expression;
        [SerializeField] public Speaker speaker;
    }
}
