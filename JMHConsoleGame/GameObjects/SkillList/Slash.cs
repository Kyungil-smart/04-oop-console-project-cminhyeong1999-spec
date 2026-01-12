public class Slash : Skill
{
    public Slash() => Init();

    private new void Init()
    {
        base.Init();
        Name = "베기";
        Damage = 20;
        Description = $"【정보】스킬:베기/데미지:{Damage}";
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