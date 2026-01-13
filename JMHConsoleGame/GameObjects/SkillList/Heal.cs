public class Heal : Skill
{
    public Heal() => Init();

    private new void Init()
    {
        base.Init();
        AddHealth = 10;
        Name = "회복";
        Damage = 0;
        ManaCost = 3;
        Description = $"【정보】스킬:회복/최대회복력:{AddHealth}/MP:{ManaCost}";
    }

    public int SpentMana()
    {
        return ManaCost;
    }

    // 스스로에게 HP 회복을 하는 주문
    public override void Execute(PlayerCharacter player, Monster target)
    {
        if (player == null) return;

        Debug.Log("Player의 회복 시전!");
            player.Heal(AddHealth);

            if (player != null && ManaCost > 0)
                player.Mana.Value -= ManaCost;
    }

    public override void Use()
    {
        // 코드 재사용 하려다 꼬여서 비전투 시 단순 설명 출력 용으로 사용중
        if (!IsBattle)
        {
            Debug.Log(Description);
        }
    }
}