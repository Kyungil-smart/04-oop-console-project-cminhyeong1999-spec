public class CreditScene : Scene
{
    public CreditScene() => Init();

    private MenuList _creditMenu;

    public void Init()
    {
        _creditMenu = new MenuList();
        _creditMenu.Add("제작자 : 조민형",()=>{});
        _creditMenu.Add("Special Thanks : 김재성 강사님, 최영민 강사님",()=>{});
        _creditMenu.Add("타이틀로 돌아가기",ChangeTitle);
    }

    public override void Enter()
    {
        _creditMenu.Reset();
    }

    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _creditMenu.SelectUp();
        } 
        
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _creditMenu.SelectDown();
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _creditMenu.Select();
        }
    }

    public override void Render()
    {
        _creditMenu.Render(8, 5);
    }

    public override void Exit()
    {
        
    }

    public void ChangeTitle()
    {
        SceneManager.Change("Title");
    }
}