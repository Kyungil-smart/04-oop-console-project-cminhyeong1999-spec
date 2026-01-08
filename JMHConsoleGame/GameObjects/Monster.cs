public class Monster : GameObject, IInteractable
{
    public Monster() => Init();

    public void Init()
    {
        Symbol = 'M';
    }

    public void Interact(PlayerCharacter player)
    {
        
    }
}