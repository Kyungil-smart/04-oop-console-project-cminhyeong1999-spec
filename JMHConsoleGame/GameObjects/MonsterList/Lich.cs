using System.Runtime.InteropServices.Marshalling;

public class Lich : Monster
{
    public Lich() => Init();

    public override void Init()
    {
        Symbol = 'L';
        MonsterName = "리치";
        _monsterHP = 500;
        _monsterAttackValue = 3;
    }
}