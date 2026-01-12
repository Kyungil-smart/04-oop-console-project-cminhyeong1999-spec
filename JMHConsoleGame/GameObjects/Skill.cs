using System.ComponentModel;

public class Skill : Item , IInteractable
{
    public Skill() => Init();
    public SkillInven _skillinven;

    public override void Use()
    {
        
    }

    private void Init()
    {
        Symbol = 'K';
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddSkill(this);
    }
}