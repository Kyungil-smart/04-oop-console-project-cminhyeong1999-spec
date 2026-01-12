

public class Potion : Item, IInteractable
{

    public Potion() => Init();
    
    private void Init()
    {
        Symbol = 'I';
        Name = "Potion";
    }

    public override void Use()
    {
        Owner.Heal(5);
        
        Inventory.Remove(this);
        Inventory = null;
        Owner = null;
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddItem(this);
    }
}