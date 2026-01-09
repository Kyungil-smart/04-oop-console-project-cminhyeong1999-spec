public class BattleScene : Scene
{
    private PlayerCharacter _player;
    private Monster _monster;
    private MenuList _battleMenu;
    private Monster _TestMonster = new Monster();

    public BattleScene(PlayerCharacter player, Monster monster) => Init(player,monster);

    public void Init(PlayerCharacter player, Monster monster)
    {
        _player = player;
        _monster = monster;
        _battleMenu = new MenuList();
        _battleMenu.Add("공격",()=>{});
        _battleMenu.Add("도망치기",BattleQuit);
    }

    public override void Enter()
    {
        _battleMenu.Reset();
        Debug.Log("타이틀 씬 진입");
    }

    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _battleMenu.SelectUp();
        } 
        
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _battleMenu.SelectDown();
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _battleMenu.Select();
        }
    }

    public override void Render()
    {        
        _battleMenu.Render(8, 5);

    }

    public override void Exit()
    {
        
    }

    public void BattleQuit()
    {
        SceneManager.Change("Town");
    }
}