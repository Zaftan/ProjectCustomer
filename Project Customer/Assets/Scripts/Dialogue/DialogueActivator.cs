using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData data;

    public void Interact(PlayerMovement player)
    {
        player.dialogueUI.ShowDialogue(data);
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