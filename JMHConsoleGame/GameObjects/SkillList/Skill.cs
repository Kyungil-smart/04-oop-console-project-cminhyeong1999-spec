using System.ComponentModel;

public class Skill : Item , IInteractable
{
    public int Damage { get; set; }
    public int ManaCost { get; set; }
    public int AddHealth { get; set; }
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
        // 코드 재사용 하려다 꼬여서 만약에 Use가 호출되면 단순 출력 용으로 사용중
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
        if (monster == null) return;
        monster.TakeDamage(Damage);
    }

    // 스킬의 고유 행동을 구현. 기본은 몬스터에게 데미지 주고 마나 차감.
    public virtual void Execute(PlayerCharacter player, Monster monster)
    {
        if (monster != null && Damage > 0)
        {
            Attack(monster, Damage);
        }

        if (player != null && ManaCost > 0)
        {
            player.Mana.Value -= ManaCost;
        }
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddSkill(this); 
    }
}