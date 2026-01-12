

public class TownScene : Scene
{
    private static readonly int _mapWidth = 80;
    private static readonly int _mapHeight = 24;
    private Tile[,] _field = new Tile[_mapHeight, _mapWidth];
    private PlayerCharacter _player;
    
    public TownScene(PlayerCharacter player) => Init(player);

    public void Init(PlayerCharacter player)
    {
        _player = player;
        
        for (int y = 0; y < _field.GetLength(0); y++)
        {
            for (int x = 0; x < _field.GetLength(1); x++)
            {
                Vector pos = new Vector(x, y);
                _field[y, x] = new Tile(pos);
            }
        }
        _field[8,8].OnTileObject = new Monster();
        _field[6,6].OnTileObject = new Skill(){Name = "테스트스킬"};

        SetWall();
    }

    public override void Enter()
    {
        _player.Field = _field;
        if( (_player?.Position.X == null || _player?.Position.Y == null) ||
            (_player.Position.X == 0    || _player.Position.Y == 0   )    ) 
            _player.Position = new Vector(4, 2);
        
        _field[_player.Position.Y, _player.Position.X].OnTileObject = _player;
        
        Debug.Log("타운 씬 진입");
    }

    public override void Update()
    {
        _player.Update();
    }

    public override void Render()
    {
        //PrintField();
        PrintCameraView();
        _player.Render();
    }

    public override void Exit()
    {
        _field[_player.Position.Y, _player.Position.X].OnTileObject = null;
        _player.Field = null;
    }

    private void PrintField()
    {
        for (int y = 0; y < _field.GetLength(0); y++)
        {
            for (int x = 0; x < _field.GetLength(1); x++)
            {
                _field[y, x].Print();
            }
            Console.WriteLine();
        }
    }

    private void PrintCameraView()
    {
        const int view_width = 40;
        const int view_height = 12;
        // 플레이어가 있으면 플레이어, 플레이어가 없으면 맵의 (0,0) 기준
        int centerX = _player?.Position.X ?? 0;
        int centerY = _player?.Position.Y ?? 0;

        // 화면 출력을 위한 시작점 계산
        int camLeft = centerX - (view_width / 2);
        int camTop = centerY - (view_height / 2);

        // 클램프 설정
        if (camLeft < 0) camLeft = 0;
        if (camTop < 0) camTop = 0;
        if (camLeft + view_width > _field.GetLength(1)) camLeft = _field.GetLength(1) - view_width;
        if (camTop + view_height > _field.GetLength(0)) camTop = _field.GetLength(0) - view_height;

        // 테두리 출력을 통해 현재 출력되는 화면을 식별하기 용이하게끔 함
        for (int y = 0; y < view_height + 2; y++)
        {
            Console.SetCursorPosition(29, y);
            for (int x = 0; x < view_width + 2; x++)
            {
                if(x == 0 && y == 0) '┌'.Print();
                else if (x == view_width + 1 && y == 0) '┐'.Print();
                else if (x == 0 && y == view_height + 1) '└'.Print();
                else if (x == view_width + 1 && y == view_height + 1) '┘'.Print();
                else if (x == 0 || x == view_width + 1) '│'.Print();
                else if (y == 0 || y == view_height + 1) '─'.Print();
                else ' '.Print();
            }
            Console.WriteLine();
        }

        for (int y = 0; y < view_height; y++)
        {
            Console.SetCursorPosition(30, y + 1);
            int fieldY = camTop + y;

            for (int x = 0; x < view_width; x++)
            {
                int fieldX = camLeft + x;
                _field[fieldY, fieldX].Print();
            }
            Console.WriteLine();
        }
        
    }

    private void SetWall()
    {
        if(_field == null) return;

        for(int i = 0; i < _field.GetLength(0); i++)
        {
            for(int j = 0; j<_field.GetLength(1); j++)
            {
                if(i == 0 || i == _field.GetLength(0) - 1) _field[i,j].OnTileObject = new Wall();
                else if (j == 0 || j == _field.GetLength(1) - 1) _field[i,j].OnTileObject = new Wall();
            }
        }
    }

    private void PrintEmoji()
    {
        
    }
}