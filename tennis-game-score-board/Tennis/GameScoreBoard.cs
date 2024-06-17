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
        var refereeInput = ReadRefereeInput();

        if (refereeInput == "$ score 1") {
            _notifier.Notify("Fifteen Love");
        }
        else {
            _notifier.Notify("Love Fifteen");
        }
    }

    private string ReadRefereeInput()
    {
        return _reader.Read();
    }
}