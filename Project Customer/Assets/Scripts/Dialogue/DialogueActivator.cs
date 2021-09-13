using UnityEngine;
using UnityEngine.Events;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData data;

    //outline stuff
    private Outline outline;
    [Header("Outline object when in range")]
    [SerializeField] private bool useOutline = false;

    [Header("Event fired on interaction")]
    [SerializeField] private UnityEvent interactEvent;
    [Header("Event fired after interaction")]
    [SerializeField] private UnityEvent endEvent;
    [Header("Locked event, can be unlocked and attached to end event")]
    [SerializeField] private UnityEvent lockedEvent;

    private void Start()
    {
        if (useOutline)
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
        }
    }

    public void Interact(PlayerMovement player)
    {
        //initailize response events
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.dialogueData == data)
            {
                player.dialogueUI.AddResponseEvents(responseEvents.events);
                break;
            }
        }

        //set activator reference
        player.dialogueUI.activator = this;
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
    public void UnLockEvent()
    {
        endEvent.AddListener(() => lockedEvent?.Invoke());
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
            ToggleOutline();
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
                ToggleOutline();
            }
        }
    }

    private void ToggleOutline()
    {
        if (useOutline)
        {
            outline.enabled = !outline.enabled;
        }
    }
}