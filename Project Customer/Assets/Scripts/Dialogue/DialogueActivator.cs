using UnityEngine;
using UnityEngine.Events;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData data;

    [Header("Event fired on interaction")]
    [SerializeField] private UnityEvent interactEvent;
    [Header("Event fired after interaction")]
    [SerializeField] private UnityEvent endEvent;

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.interactable = null;
            }
        }
    }
}