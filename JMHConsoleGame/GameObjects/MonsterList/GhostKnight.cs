public class GhostKnight : Monster
{
    public GhostKnight() => Init();

    public override void Init()
    {
        Symbol = 'k';
        MonsterName = "유령기사";
        _monsterHP = 60;
        _monsterAttackValue = 2;
    }
}