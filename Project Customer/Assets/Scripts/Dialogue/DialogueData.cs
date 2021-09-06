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
    public Response[] responseData => responses;
    public bool HasResponses => responses != null && responses.Length > 0;

    [System.Serializable]
    public struct DialogueMessage
    {
        [SerializeField] [TextArea] public string dialogue;
        [SerializeField] public string test;
    }
}
