using System.Reflection.Metadata.Ecma335;

public class Ghost : Monster
{
    public Ghost() => Init();

    public override void Init()
    {
        Symbol = 'G';
        MonsterName = "유령";
        _monsterHP = 50;
        _monsterAttackValue = 1;
    }
}