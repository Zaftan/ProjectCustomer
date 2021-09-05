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

    private List<GameObject> tempButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        //get object refs
        box = transform.Find("ResponseBox").GetComponent<RectTransform>();
        container = box.Find("ResponseContainer").GetComponent<RectTransform>();
    }

    public void ShowResponses(Response[] responses)
    {
        float boxHeight = 0;
        foreach (Response response in responses)
        {
            //build new button
            GameObject responseButton = Instantiate(buttonTemplate, container).gameObject;
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.title;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            tempButtons.Add(responseButton);
            //increment height
            boxHeight += buttonTemplate.sizeDelta.y;
        }
        box.sizeDelta = new Vector2(box.sizeDelta.x, boxHeight);
        box.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        //reset buttons
        box.gameObject.SetActive(false);
        foreach (GameObject button in tempButtons)
        {
            Destroy(button);
        }
        tempButtons.Clear();
        //activate button event
        dialogueUI.ShowDialogue(response.data);
    }
}