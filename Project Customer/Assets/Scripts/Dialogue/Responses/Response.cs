using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseTitle;
    [SerializeField] private DialogueData dialogueData;

    //public get references
    public string title => responseTitle;
    public DialogueData data => dialogueData;
}
