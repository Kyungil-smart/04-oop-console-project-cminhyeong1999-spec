// 아이템 습득, 버리기, 전투 돌입, 데미지 표시 등등의 로그들을 일괄적으로 출력하기 위한 클래스
// Queue구조를 사용하여 로그 카운트가 일정 크기 이상이면 처음 들어온 로그를 삭제

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
        // 화면에 14개의 로그까지만 출력하도록 조절
        // 로그 선 추가 후 15개 이상부터 14개가 될때까지 처음 받은 로그 삭제
        _logList.Enqueue((LogType.Normal, text));
        if(_logList.Count > 14)
        {
            while(!(_logList.Count <= 14))
            {
                _logList.Dequeue();
            }
        }
    }

    public static void LogWarning(string text)
    {
        // 화면에 14개의 로그까지만 출력하도록 조절
        // 로그 선 추가 후 15개 이상부터 14개가 될때까지 처음 받은 로그 삭제
        _logList.Enqueue((LogType.Warning, text));
        if(_logList.Count > 14)
        {
            while(!(_logList.Count <= 14))
            {
                _logList.Dequeue();
            }
        }
    }

    public static void Render()
    {
        // 로그UI에 로그를 출력하기 위해 커서를 옮김
        Console.SetCursorPosition(73,15);

        // 일반 로그는 노란색, 위험 로그는 빨간색으로 표시
        int _y = 15;
        foreach(var index in _logList)
        {
            if(_y > 30) break;
            Console.SetCursorPosition(73,_y);
            if(index.type==LogType.Normal) index.text.Print(ConsoleColor.Yellow);
            else if (index.type==LogType.Warning) index.text.Print(ConsoleColor.Red);
            // 로그 출력 후 y좌표 증가시켜 다음 로그를 표시하기 위한 준비
            _y++;
        }

    }
}