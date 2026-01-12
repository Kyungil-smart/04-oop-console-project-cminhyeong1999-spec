

public static class Debug
{
    public enum LogType
    {
        Normal,
        Warning
    }
    
    private static Queue<(LogType type, string text)> _logList = new Queue<(LogType type, string text)>();

    public static void Log(string text)
    {
        _logList.Enqueue((LogType.Normal, text));
    }

    public static void LogWarning(string text)
    {
        _logList.Enqueue((LogType.Warning, text));
    }

    public static void Render()
    {
        Console.SetCursorPosition(73,15);
        if(_logList.Count > 14) _logList.Dequeue();

        int _y = 15;
        foreach(var index in _logList)
        {
            if(_y > 30) break;
            Console.SetCursorPosition(73,_y);
            index.text.Print(ConsoleColor.Yellow);
            _y++;
        }

    }
}