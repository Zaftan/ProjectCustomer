using UnityEngine;
using UnityEngine.Events;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData data;
    private Outline outline;

    [Header("Event fired on interaction")]
    [SerializeField] private UnityEvent interactEvent;
    [Header("Event fired after interaction")]
    [SerializeField] private UnityEvent endEvent;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact(PlayerMovement player)
    {
        interactEvent?.Invoke();
        player.dialogueUI.ShowDialogue(data);
    }

    //events
    public void OnInteract()
    {
        interactEvent?.Invoke();
    }

    public void OnEndInteract()
    {
        endEvent?.Invoke();
    }

    //change dialogue object at runtime method
    public void UpdateDialogueData(DialogueData data)
    {
        this.data = data;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.interactable = this;
            //outline object
            outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.interactable = null;
                //remove object outline
                outline.enabled = false;
            }
        }
    }
}