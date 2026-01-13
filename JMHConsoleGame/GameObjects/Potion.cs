

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
        if(Owner.Health.Value >= 25)
        {
            Debug.LogWarning("HP가 꽉 차있어 사용할 수 없음...");
            return;
        }
        else if (Owner.Health.Value < 25 && Owner.Health.Value >= 21)
        {
            Debug.Log($"HP가 조금 차오르는 기분이다...");
            Owner.Health.Value = 25;
            Inventory.Remove(this);
            Inventory = null;
            Owner = null;
            return;
        }
        else
        {
            Owner.Heal(5);
        
            Inventory.Remove(this);
            Inventory = null;
            Owner = null;
            return;   
        }
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddItem(this);
    }
}