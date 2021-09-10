using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogueData data;
    [SerializeField] private ResponseEvent[] responseEvents;

    public ResponseEvent[] events => responseEvents;
    public DialogueData dialogueData => data;

    public void OnValidate()
    {
        //are there responses to attach events to?
        if (data == null) return;
        if (data.responsesData == null) return;
        //are events already generated?
        if (responseEvents != null && responseEvents.Length == data.responsesData.Length) return;

        //initialize event array
        if (responseEvents == null)
        {
            responseEvents = new ResponseEvent[data.responsesData.Length];
        }
        else
        {
            Array.Resize(ref responseEvents, data.responsesData.Length);
        }

        //generate events for responses
        for (int i = 0; i < data.responsesData.Length; i++)
        {
            Response response = data.responsesData[i];
            if (responseEvents[i] != null)
            {
                //update event
                responseEvents[i].name = response.title;
            }
            else
            {
                //create new event
                responseEvents[i] = new ResponseEvent() { name = response.title };
            }
        }
    }
}