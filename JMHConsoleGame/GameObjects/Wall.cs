using System.ComponentModel;

public class Wall : GameObject
{
    public Wall() => Init();

    public void Init()
    {
        Symbol = 'W';
    }
}