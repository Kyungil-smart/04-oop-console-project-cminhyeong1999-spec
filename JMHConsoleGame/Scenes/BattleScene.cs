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

        AddAttackMenu();
        _attackMenu.Add("뒤로",ChangeAttackMode);
        _player.UseSkillMode();
    }

    private void AddAttackMenu()
    {
        if(_player._skillinven == null) return;

        foreach(var index in _player._skillinven._skills)
        {
            _attackMenu.Add(index.Name,index.Use);
        }
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
        Printboundary();
        PrintHowToPlay();
        _player.DrawHealthGauge();
        _player.DrawManaGauge();
        _player._inventory.Render();
        if (IsAttacked == true)
        {
            _attackMenu.Render(30,16);
        }
        else
        {
            _battleMenu.Render(30,16);
        }

    }

    public override void Exit()
    {
        _player.UnUseSkillMode();
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