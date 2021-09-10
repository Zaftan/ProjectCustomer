using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    private RectTransform box;
    private RectTransform container;
    [SerializeField] private RectTransform buttonTemplate;

    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;

    private readonly List<GameObject> tempButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        //get object refs
        box = transform.Find("ResponseBox").GetComponent<RectTransform>();
        container = box.Find("ResponseContainer").GetComponent<RectTransform>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        float boxHeight = 0;
        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;
            //build new button
            GameObject responseButton = Instantiate(buttonTemplate, container).gameObject;
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.title;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));
            tempButtons.Add(responseButton);
            //increment height
            boxHeight += buttonTemplate.sizeDelta.y;
        }
        box.sizeDelta = new Vector2(box.sizeDelta.x, boxHeight);
        box.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        //reset buttons
        box.gameObject.SetActive(false);
        foreach (GameObject button in tempButtons)
        {
            Destroy(button);
        }
        tempButtons.Clear();

        //check if event is in bounds
        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].onPickedResponse?.Invoke();
        }
        responseEvents = null; //prevent carrying of events between dialogues on same object

        //only show response if it has dialogue
        if (response.data)
        {
            //activate button events
            dialogueUI.ShowDialogue(response.data);
        }
        else
        {
            //end dialogue
            dialogueUI.EndDialogue();
        }
    }
}