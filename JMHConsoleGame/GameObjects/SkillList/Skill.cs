using System.ComponentModel;

public class Skill : Item , IInteractable
{
    public int Damage { get; set; }
    public Skill() => Init();
    public SkillInven _skillinven;
    public bool IsBattle;

    public void Init()
    {
        Symbol = 'S';
        IsBattle = false;
    }

    public override void Use()
    {
        // 코드 재사용 하려다 꼬여서 비전투 시 단순 설명 출력 용으로 사용중
        if (!IsBattle)
        {
            Debug.Log("스킬설명");
        }
        else
        {
            Debug.Log("스킬사용");
        }
    }

    public virtual void Attack(Monster monster,int Damage)
    {
        monster.TakeDamage(Damage);
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddSkill(this); 
    }
}