using System.ComponentModel;

public class Skill : Item
{
    public Skill() => Init();

    private void Init()
    {
        Symbol = 'K';
    }

    public override void Use()
    {
        throw new NotImplementedException();
    }
    
    public void Interact(PlayerCharacter player)
    {
        player.AddItem(this);
    }
}