using System.ComponentModel;

public class Skill : Item , IInteractable
{
    private Random _randomDamage;
    public int Damage { get; set; }
    public Skill() => Init();
    public SkillInven _skillinven;


    public override void Use()
    {
        _randomDamage.Next(-5,5);
    }

    private void Init()
    {
        Symbol = 'K';
        _randomDamage = new Random();
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddSkill(this);
    }
}