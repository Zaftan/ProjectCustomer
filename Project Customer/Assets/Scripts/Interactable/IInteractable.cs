public interface IInteractable
{
    public void Interact(PlayerMovement player);

    //events
    public void OnInteract();
    public void OnEndInteract();
}