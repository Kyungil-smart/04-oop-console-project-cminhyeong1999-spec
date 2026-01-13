public class Fireball : Skill
{
    public Fireball() => Init();

    private new void Init()
    {
        base.Init();
        Name = "파이어볼";
        Damage = 50;
        ManaCost = 5;
        AddHealth = 0;
        Description = $"【정보】스킬:파이어볼/데미지:{Damage}/MP:{ManaCost}";
    }

    // 예시: 기본 단일 대상 데미지 + 로그 (추후 범위 공격 등 확장 가능)
    public override void Execute(PlayerCharacter player, Monster target)
    {
        Debug.Log("Player의 파이어볼 시전!");
        base.Attack(target, Damage);

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