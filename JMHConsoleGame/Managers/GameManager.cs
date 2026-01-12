using System;

public class GameManager
{
    public static bool IsGameOver { get; set; }
    public const string GameName = "아무튼 RPG";
    private PlayerCharacter _player;

    public void Run()
    {
        // 커서 깜빡임을 off로 하여 화면 깜빡임을 약간이나마 줄이는 용도
        Console.CursorVisible = false;
        Init();
        
        while (!IsGameOver)
        {
            Console.Clear();
            // 렌더링
            SceneManager.Render();
            // 키입력 받고
            InputManager.GetUserInput();

            if (InputManager.GetKey(ConsoleKey.L))
            {
                SceneManager.Change("Log");
            }

            // 데이터 처리
            SceneManager.Update();
        }
    }

    private void Init()
    {
        IsGameOver = false;
        SceneManager.OnChangeScene += InputManager.ResetKey;
        _player = new PlayerCharacter();
        
        SceneManager.AddScene("Title", new TitleScene());
        //SceneManager.AddScene("Story", new StoryScene());
        SceneManager.AddScene("Town", new TownScene(_player));
        SceneManager.AddScene("Credit", new CreditScene());
        
        SceneManager.Change("Title");
    }
}