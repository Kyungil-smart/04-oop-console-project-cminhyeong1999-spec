public class Ripper : Monster
{
    public Ripper() => Init();

    public override void Init()
    {
        Symbol = 'R';
        MonsterName = "리퍼";
        _monsterHP = 25;
        _monsterAttackValue = 4;
    }
}