public class Monster : GameObject, IInteractable
{
    public Monster() => Init();
    public string MonsterName = "몬스터클래스 테스트";

    public void Init()
    {
        Symbol = 'M';
    }

    public void Interact(PlayerCharacter player)
    {
        
    }
}