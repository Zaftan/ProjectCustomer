using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] dialogueText => dialogue;
    //response getters
    public Response[] responseData => responses;
    public bool HasResponses => responses != null && responses.Length > 0;
}
