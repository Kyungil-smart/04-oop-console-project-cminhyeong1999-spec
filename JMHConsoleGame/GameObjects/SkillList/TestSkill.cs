public class TestSkill : Skill
{
    public TestSkill() => Init();

    private new void Init()
    {
        base.Init();
        Name = "테스트용한방죽창";
        Damage = 9999;
        AddHealth = 0;
        ManaCost = 0;
        Description = $"{Damage}데미지로한방컷";
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