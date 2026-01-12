using System.Runtime.CompilerServices;

public class BattleScene : Scene
{
    private bool IsAttacked;
    private PlayerCharacter _player;
    private Monster _monster;
    private MenuList _battleMenu;
    private MenuList _attackMenu;
    private Monster _TestMonster = new Monster();

    public BattleScene(PlayerCharacter player, Monster monster) => Init(player,monster);

    public void Init(PlayerCharacter player, Monster monster)
    {
        IsAttacked = false;

        _player = player;
        _monster = monster;

        _battleMenu = new MenuList();
        _attackMenu = new MenuList();

        _battleMenu.Add("공격",Attack);
        _battleMenu.Add("도망치기",BattleQuit);

        _attackMenu.Add("몸통박치기",()=>{});
        _attackMenu.Add("뒤로",ChangeAttackMode);
    }

    public override void Enter()
    {
        _battleMenu.Reset();
    }

    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            if (IsAttacked == true)
            {
                _attackMenu.SelectUp();
            }
            else
            {
                _battleMenu.SelectUp();
            }
        } 
        
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            if (IsAttacked == true)
            {
                _attackMenu.SelectDown();
            }
            else
            {
                _battleMenu.SelectDown();
            }
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            if (IsAttacked == true)
            {
                _attackMenu.Select();
            }
            else
            {
                _battleMenu.Select();
            }
        }
    }

    public override void Render()
    {        
        if (IsAttacked == true)
        {
            _attackMenu.Render(8,5);
        }
        else
        {
            _battleMenu.Render(8,5);
        }

    }

    public override void Exit()
    {
        _player = null;
        _monster = null;
    }

    public void Attack()
    {
        ChangeAttackMode();
    }

    public void ChangeAttackMode()
    {
        IsAttacked = !IsAttacked;
    }

    public void BattleQuit()
    {
        SceneManager.Change("Town");
    }
}