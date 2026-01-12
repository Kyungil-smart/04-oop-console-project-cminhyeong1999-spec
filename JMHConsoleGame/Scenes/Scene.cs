

public abstract class Scene
{
    public abstract void Enter();
    public abstract void Update();
    public abstract void Render();
    public abstract void Exit();

    // 테두리 출력을 통해 현재 출력되는 화면을 식별하기 용이하게끔 함
    // 여러 씬에서 공통의 테두리를 출력하여 일관성 있는 게임 UI를 출력하기 위하여 부모클래스에 메서드 구현
    // cmd의 size가 120 x 30 임을 감안하여 Map UI를 대략 중앙 즈음에 위치하게끔 조정
    public void Printboundary()
    {
        const int view_width = 40;
        const int view_height = 12;
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
    }

    public void PrintHowToPlay()
    {
        Ractangle _menualboundary = new Ractangle(72,0,40,14);
        _menualboundary.Draw();
        Console.SetCursorPosition(73,1);
        Console.WriteLine("조작 방법");
        Console.SetCursorPosition(73,2);
        Console.WriteLine("I : 가방 활성화 On/Off");
        Console.SetCursorPosition(73,3);
        Console.WriteLine("K : 스킬창 활성화 On/Off");
        Console.SetCursorPosition(73,4);
        Console.WriteLine("↑ : 플레이어/선택창 위로 움직임");
        Console.SetCursorPosition(73,5);
        Console.WriteLine("↓ : 플레이어/선택창 아래로 움직임");
        Console.SetCursorPosition(73,6);
        Console.WriteLine("← : 플레이어 왼쪽으로 움직임");
        Console.SetCursorPosition(73,7);
        Console.WriteLine("→ : 플레이어 오른쪽으로 움직임");
    }
}