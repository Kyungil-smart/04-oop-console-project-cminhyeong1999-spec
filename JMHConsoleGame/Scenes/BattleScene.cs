using System.Runtime.CompilerServices;

public class BattleScene : Scene
{
    private bool IsAttacked;
    private Tile[,] _originField;
    private PlayerCharacter _player;
    private Monster _monster;
    private MenuList _battleMenu;
    private MenuList _attackMenu;
    private Monster _TestMonster = new Monster();

    public BattleScene(PlayerCharacter player, Monster monster, Tile[,] originField) => Init(player,monster,originField);

    public void Init(PlayerCharacter player, Monster monster, Tile[,] originField)
    {
        IsAttacked = false;

        _player = player;
        _monster = monster;
        _originField = originField;

        _battleMenu = new MenuList();
        _attackMenu = new MenuList();

        _battleMenu.Add("공격",Attack);
        _battleMenu.Add("도망치기",BattleQuit);

        AddAttackMenu();
        _attackMenu.Add("뒤로",ChangeAttackMode);
        _player.UseSkillMode();
        Debug.Log($"【전투】{_monster.MonsterName} 전투 시작!");
    }

    private void AddAttackMenu()
    {
        if(_player._skillinven == null) return;

        foreach(Skill index in _player._skillinven._skills)
        {
            _attackMenu.Add(index.Name,() => AttackToMonster(index));
        }
    }

    private void AttackToMonster(Skill _skill)
    {
        if (_monster == null || _player == null) return;

        // 만약 비전투상태에 어쩌다가 몬스터 타격 메뉴가 활성화 될 경우
        // 아무 문장이라도 출력하게끔 설정
        if (!_skill.IsBattle)
        {
            _skill.Use();
            return;
        }

        // 실제 데미지 적용
        Debug.Log($"Player의 {_skill.Name} 사용!");
        _skill.Attack(_monster, _skill.Damage);

        // 몬스터 사망 처리
        if (_monster.IsDead)
        {
            Debug.Log($"【전투】{_monster.MonsterName} 처치!");
            RemoveMonsterFromField(_monster, _player);
            SceneManager.Change("Town");
            return;
        }

        // 살아 있으면 몬스터 반격
        _monster.Attack(_player);
    }

    // 플레이어 필드에서 해당 몬스터 인스턴스를 찾아 제거
    private void RemoveMonsterFromField(Monster monster, PlayerCharacter player)
    {
        if (monster == null || player == null) return;

        // 씬 전환시 player.Field가 null이 되는 현상이 있었음
        // 이를 보완하기 위해 player.Field가 null 이면 원본 필드를 참조
        var field = player?.Field ?? _originField;

        if (field == null) return;

        int h = field.GetLength(0);
        int w = field.GetLength(1);

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                var obj = field[y, x].OnTileObject;
                if (ReferenceEquals(obj, monster))
                {
                    field[y, x].OnTileObject = null;
                    return;
                }
            }
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
        PrintPlayerVsMonster();
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
        PrintLog();
    }

    public override void Exit()
    {
        Debug.Log($"【전투】{_monster.MonsterName} 전투 종료");
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

    public void PrintPlayerVsMonster()
    {
        Console.SetCursorPosition(40,6);
        _player.Symbol.Print();
        Console.SetCursorPosition(60,6);
        _monster.Symbol.Print();
        Console.SetCursorPosition(58,7);
        Console.Write($"HP:{_monster.GetHP()}");
    }

    public void BattleQuit()
    {
        SceneManager.Change("Town");
    }
}