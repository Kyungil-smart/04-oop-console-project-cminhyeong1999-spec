using System.ComponentModel;

public class Skill : Item , IInteractable
{
    private Random _randomDamage;
    public int Damage { get; set; }
    public Skill() => Init();
    public SkillInven _skillinven;
    public bool IsBattle;

    private void Init()
    {
        Symbol = 'K';
        IsBattle = false;
        _randomDamage = new Random();
    }

    public override void Use()
    {
        if (!IsBattle)
        {
            Console.WriteLine(Description);
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("사용했음");
            Console.ReadLine();
        }
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddSkill(this); 
    }
}