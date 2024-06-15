namespace Tennis;

public class GameScoreBoard
{
    private readonly Reader _reader;
    private readonly Notifier _notifier;

    public GameScoreBoard(Reader reader, Notifier notifier)
    {
        _reader = reader;
        _notifier = notifier;
    }

    public void StartGame()
    {
        _notifier.Notify("Fifteen Love");
    }
}