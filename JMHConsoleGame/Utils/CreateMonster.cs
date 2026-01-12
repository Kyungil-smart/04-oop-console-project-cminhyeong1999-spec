public class CreateMonster
{
    public static Monster Create(Monster.MonsterType type)
    {
        switch (type)
        {
            case Monster.MonsterType.Ghost:
                return new Ghost();
            case Monster.MonsterType.GhostKnight:
                return new GhostKnight();
            case Monster.MonsterType.Zombie:
                return new Zombie();
            case Monster.MonsterType.Ripper:
                return new Ripper();
            case Monster.MonsterType.Lich:
                return new Lich();
            default:
                return null;
        }
    }
}