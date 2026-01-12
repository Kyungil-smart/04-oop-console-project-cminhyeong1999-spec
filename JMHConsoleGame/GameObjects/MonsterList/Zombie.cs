public class Zombie : Monster
{
    public Zombie() => Init();

    public override void Init()
    {
        Symbol = 'Z';
        MonsterName = "좀비";
        _monsterHP = 150;
        _monsterAttackValue = 0;
    }
}