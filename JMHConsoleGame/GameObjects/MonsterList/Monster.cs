public class Monster : GameObject, IInteractable
{
    public enum MonsterType
    {
        Ghost,
        GhostKnight,
        Zombie,
        Ripper,
        Lich
    }
    protected int _monsterHP;
    protected int _monsterAttackValue;
    public Monster() => Init();
    public string MonsterName;
    public MonsterType _type;

    public virtual void Init()
    {
        Symbol = 'M';
    }

    public virtual void Attack(PlayerCharacter player)
    {
        Debug.Log($"{MonsterName}의 공격!");
        if (player != null)
        {
            player?.Damage(_monsterAttackValue);
        }
    }

    public virtual void TakeDamage(int _damage)
    {
        Debug.Log($"{MonsterName}은 {_damage}의 피해를 받았다!");
        _monsterHP -= _damage;
    }

    public void Interact(PlayerCharacter player)
    {
        SceneManager.Change(new BattleScene(player, this, player.Field));
    }

    public int GetHP() => _monsterHP;
    public bool IsDead => _monsterHP <= 0;
}