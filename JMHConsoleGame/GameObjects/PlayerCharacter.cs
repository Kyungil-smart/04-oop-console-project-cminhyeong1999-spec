

using System.Runtime.InteropServices.Marshalling;

public class PlayerCharacter : GameObject
{
    public ObservableProperty<int> Health = new ObservableProperty<int>(5);
    public ObservableProperty<int> Mana = new ObservableProperty<int>(5);
    private string _healthGauge;
    private string _manaGauge;
    private int _attackValue;
    
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
        _healthGauge = "■■■■■";
        _manaGauge = "■■■■■";
        _attackValue = 10;
        _inventory = new Inventory(this);
        _skillinven = new SkillInven(this);
        _skillinven.Add(new Skill{Name="베기",Description="베기동작"});
        _skillinven.Add(new Skill{Name="찌르기",Description="찌르기동작"});
        _skillinven.Add(new Skill{Name="휘두르기",Description="휘두르기동작"});
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
        
        //Vector current = Position;
        Vector nextPos = Position + direction;

        GameObject nextTileObject = Field[nextPos.Y, nextPos.X].OnTileObject;

        // 1. 맵 바깥은 아닌지?
        if ( nextPos.Y < Field[0,0].Position.Y || nextPos.X < Field[0,0].Position.X || nextPos.Y >= Field.GetLength(0) || nextPos.X >= Field.GetLength(1))
        {
            return;
        }
        // 2. 벽인지?
        if(nextTileObject?.Symbol == 'W')
        {
            return;
        }

        if (nextTileObject != null)
        {
            if (nextTileObject is IInteractable)
            {
                (nextTileObject as IInteractable).Interact(this);
                if (Field == null) return;
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
        Debug.Log($"【아이템】 {item.Name} 획득!");
    }

    public void AddSkill(Skill skill)
    {
        _skillinven.Add(skill);
        Debug.Log($"【스킬】 {skill.Name} 획득!");
    }

    public void DrawHealthGauge()
    {
        Console.SetCursorPosition(30, 14);
        Console.Write("HP ");
        _healthGauge.Print(ConsoleColor.Red);
    }

    public void DrawManaGauge()
    {
        Console.SetCursorPosition(30, 15);
        Console.Write("MP ");
        _healthGauge.Print(ConsoleColor.Blue);
    }

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

    public void SetHealthGauge(int health)
    {
        switch (health)
        {
            case 5:
                _healthGauge = "■■■■■";
                break;
            case 4:
                _healthGauge = "■■■■□";
                break;
            case 3:
                _healthGauge = "■■■□□";
                break;
            case 2:
                _healthGauge = "■■□□□";
                break;
            case 1:
                _healthGauge = "■□□□□";
                break;
        }
    }

    public void SetManaGauge(int mana)
    {
        switch (mana)
        {
            case 5:
                _healthGauge = "■■■■■";
                break;
            case 4:
                _healthGauge = "■■■■□";
                break;
            case 3:
                _healthGauge = "■■■□□";
                break;
            case 2:
                _healthGauge = "■■□□□";
                break;
            case 1:
                _healthGauge = "■□□□□";
                break;
        }
    }

    public void Heal(int value)
    {
        Health.Value += value;
        Debug.Log($"【회복】 HP를 {value}만큼 회복!");
    }

    public void Damage(int value)
    {
        Health.Value -= value;
        Debug.Log($"【피해】 HP를 {value}만큼 피해를 받음!");
    }
}