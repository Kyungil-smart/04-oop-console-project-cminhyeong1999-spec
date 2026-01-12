

using System.Runtime.InteropServices.Marshalling;

public class PlayerCharacter : GameObject
{
    public ObservableProperty<int> Health = new ObservableProperty<int>(5);
    public ObservableProperty<int> Mana = new ObservableProperty<int>(5);
    private string _healthGauge;
    private string _manaGauge;
    private int _attackValue;
    
    public Tile[,] Field { get; set; }
    private Inventory _inventory;
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
    }

    public void Update()
    {
        if (InputManager.GetKey(ConsoleKey.I))
        {
            InventoryControl();
        }
        
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            Move(Vector.Up);
            _inventory.SelectUp();
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            Move(Vector.Down);
            _inventory.SelectDown();
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
        }

        if (InputManager.GetKey(ConsoleKey.T))
        {
            Health.Value--;
        }
    }

    public void InventoryControl()
    {
        _inventory.IsActive = !_inventory.IsActive;
        IsActiveControl = !_inventory.IsActive;
    }

    private void Move(Vector direction)
    {
        if (Field == null || !IsActiveControl) return;
        
        Vector current = Position;
        Vector nextPos = Position + direction;

        // 1. 맵 바깥은 아닌지?
        if ( nextPos.Y < Field[0,0].Position.Y || nextPos.X < Field[0,0].Position.X || nextPos.Y >= Field.GetLength(0) || nextPos.X >= Field.GetLength(1))
        {
            return;
        }
        // 2. 벽인지?
        //if ()
        //{
        //    return;
        //}

        GameObject nextTileObject = Field[nextPos.Y, nextPos.X].OnTileObject;

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
    }

    public void AddItem(Item item)
    {
        _inventory.Add(item);
    }

    public void DrawManaGauge()
    {
        Console.SetCursorPosition(0, 16);
        _healthGauge.Print(ConsoleColor.Blue);
    }

    public void DrawHealthGauge()
    {
        Console.SetCursorPosition(0, 15);
        _healthGauge.Print(ConsoleColor.Red);
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
    }
}