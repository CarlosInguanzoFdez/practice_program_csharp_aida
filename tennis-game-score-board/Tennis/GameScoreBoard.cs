namespace Tennis;

public class GameScoreBoard
{
    private readonly Reader _reader;
    private readonly Notifier _notifier;
    private readonly Score _score;

    public GameScoreBoard(Reader reader, Notifier notifier)
    {
        _reader = reader;
        _notifier = notifier;
        _score = new Score();
    }

    public void StartGame()
    {
        var refereeInput = ReadRefereeInput();

        if (refereeInput == "$ score 1") {
            Print("Fifteen Love");
        }
        else {
            Print("Love Fifteen");
        }
    }

    public void ScorePlayer1()
    {
        _score.AddPointForPlayer("$ score 1");
    }

    private void Print(string scoreBoard)
    {
        _notifier.Notify(scoreBoard);
    }

    private string ReadRefereeInput()
    {
        return _reader.Read();
    }
}

public class Score
{
    public void AddPointForPlayer(string player)
    {
        
    }
}