

using System.Runtime.InteropServices.Marshalling;

public class PlayerCharacter : GameObject
{
    public ObservableProperty<int> Health = new ObservableProperty<int>(25);
    public ObservableProperty<int> Mana = new ObservableProperty<int>(10);
    private string _healthGauge;
    private string _manaGauge;
    public int _attackValue;
    
    public Tile[,] Field { get; set; }
    public Inventory _inventory;
    public SkillInven _skillinven;
    public bool IsActiveControl { get; private set; }

    public PlayerCharacter() => Init();

    public void Init()
    {
        Symbol = 'P';
        IsActiveControl = true;
        Health.AddListener(SetHealthGauge);
        Mana.AddListener(SetManaGauge);
        _healthGauge = SetGauge(Health.Value);
        _manaGauge = SetGauge(Mana.Value);
        _attackValue = 0;
        _inventory = new Inventory(this);
        _skillinven = new SkillInven(this);
        _skillinven.Add(new Slash());
    }

    public void Update()
    {
        if (InputManager.GetKey(ConsoleKey.Escape))
        {
            GameManager.IsGameOver = true;
        }

        if (InputManager.GetKey(ConsoleKey.I))
        {
            InventoryControl();
        }

        if (InputManager.GetKey(ConsoleKey.K))
        {
            SkillInvenControl();
        }
        
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            Move(Vector.Up);
            _inventory.SelectUp();
            _skillinven.SelectUp();
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            Move(Vector.Down);
            _inventory.SelectDown();
            _skillinven.SelectDown();
        }

        if (InputManager.GetKey(ConsoleKey.LeftArrow))
        {
            Move(Vector.Left);
        }

        if (InputManager.GetKey(ConsoleKey.RightArrow))
        {
            Move(Vector.Right);
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _inventory.Select();
            _skillinven.Select();
        }
    }

    public void InventoryControl()
    {
        _inventory.IsActive = !_inventory.IsActive;
        IsActiveControl = !_inventory.IsActive;
    }

    public void SkillInvenControl()
    {
        _skillinven.IsActive = !_skillinven.IsActive;
        IsActiveControl = !_skillinven.IsActive;
    }

    public void UseSkillMode()
    {
        foreach(var index in _skillinven._skills)
        {
            if(index.IsBattle == false) index.IsBattle = true;
        }
    }

    public void UnUseSkillMode()
    {
        foreach(var index in _skillinven._skills)
        {
            if(index.IsBattle == true) index.IsBattle = false;
        }
    }

    private void Move(Vector direction)
    {
        if (Field == null || !IsActiveControl) return;
        
        Vector nextPos = Position + direction;

        // 1. 맵 바깥은 아닌지?
        if ( nextPos.Y < Field[0,0].Position.Y || nextPos.X < Field[0,0].Position.X || nextPos.Y >= Field.GetLength(0) || nextPos.X >= Field.GetLength(1))
        {
            return;
        }

        GameObject nextTileObject = Field[nextPos.Y, nextPos.X].OnTileObject;

        // 2. 벽인지?
        if(nextTileObject?.Symbol == 'W') return;

        if (nextTileObject != null)
        {
            if (nextTileObject is IInteractable)
            {
                (nextTileObject as IInteractable).Interact(this);

                if (Field == null) return;

                return;
            }
        }

        if (Field[Position.Y, Position.X].OnTileObject == this)
            Field[Position.Y, Position.X].OnTileObject = null;
        
        Field[nextPos.Y, nextPos.X].OnTileObject = this;
        Position = nextPos;
    }

    public void Render()
    {
        DrawHealthGauge();
        DrawManaGauge();
        _inventory.Render();
        _skillinven.Render();
        PrintIsInventory();
        PrintIsSkillMenu();
    }

    public void AddItem(Item item)
    {
        _inventory.Add(item);
        Debug.Log($"{item.Name} 획득!");
    }

    public void RemoveItem(Item item)
    {
        _inventory.Remove(item);
        Debug.LogWarning($"{item.Name} 삭제");
    }

    public void AddSkill(Skill skill)
    {
        if(_skillinven._skills.Count >= 6)
        {
            Debug.LogWarning($"{skill.Name} 획득 불가");
            return;
        }
        _skillinven.Add(skill);
        Debug.Log($"{skill.Name} 획득!");
    }
    public void RemoveSkill(Skill skill)
    {
        _skillinven.Remove(skill);
        Debug.LogWarning($"{skill.Name} 삭제");
    }

    // HP바 출력
    public void DrawHealthGauge()
    {
        Console.SetCursorPosition(30, 14);
        Console.Write("HP ");
        _healthGauge.Print(ConsoleColor.Red);
        Console.Write($" : {Health.Value}");
    }

    // MP바 출력
    public void DrawManaGauge()
    {
        Console.SetCursorPosition(30, 15);
        Console.Write("MP ");
        _manaGauge.Print(ConsoleColor.Blue);
        Console.Write($" : {Mana.Value}");
    }

    // 가방 활성화 여부 출력
    public void PrintIsInventory()
    {
        Console.SetCursorPosition(55, 14);
        if (_inventory.IsActive)
        {
            Console.Write("가방 활성화");
        }
        else
        {
            Console.Write("가방 비활성화");
        }
    }

    // 스킬창 활성화 여부 출력
    public void PrintIsSkillMenu()
    {
        Console.SetCursorPosition(55, 15);
        if (_skillinven.IsActive)
        {
            Console.Write("스킬창 활성화");
        }
        else
        {
            Console.Write("스킬창 비활성화");
        }
    }

    // HP 수치에 따른 HP바 상태 결정 메서드
    public void SetHealthGauge(int health)
    {
        _healthGauge = SetGauge(health);
    }

    // MP 수치에 따른 MP바 상태 결정 메서드
    public void SetManaGauge(int mana)
    {
        _manaGauge = SetGauge(mana);
    }

    // 절대값 수치를 5로 나눈 후 비율만큼 꽉찬 네모 또는 빈 네모 배열을 반환
    // ex) 16 / 5 = 3 | 5 - 3 = 2 ==> ■■■ + □□ // 21 / 5 = 4 | 5 - 4 = 1 ==> ■■■■ + □
    public string SetGauge(int value)
    {
        if(value <= 0) return "□□□□□";
        int _percent = 5;
        int setString = value / _percent;
        
        // 해당 조건을 만족하게 되면 "□□□□□"로 출력되는데
        // 이러면 hp가 없는 상황과 헷갈릴 것 같아 "■□□□□"로 출력되도록 함
        if(value >= 0 && setString <= 1) setString = 1;

        return new string('■',setString) + new string('□',_percent - setString); 
    }

    // 플레이어의 HP가 증가되었을 경우 처리 메서드
    public void Heal(int value)
    {
        Health.Value += value;
        Debug.Log($"HP를 {value}만큼 회복!");
    }

    // 플레이어가 데미지를 입었을 경우 처리 메서드
    public void Damage(int value)
    {
        Health.Value -= value;
        Debug.LogWarning($"HP를 {value}만큼 피해를 받음!");
    }
}