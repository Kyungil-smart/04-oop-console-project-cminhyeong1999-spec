public class Monster : GameObject, IInteractable
{
    //private Random _random = new Random(); // 랜덤으로 몬스터 타입을 지정하기 위한 난수
    private int _monsterHP;
    private int _monsterType;
    public Monster() => Init();
    public string MonsterName = "몬스터클래스 테스트";

    public void Init()
    {
        Symbol = 'M';
        _monsterType = 0;
    }

    public void Interact(PlayerCharacter player)
    {
        SceneManager.Change(new BattleScene(player, this));
    }
}