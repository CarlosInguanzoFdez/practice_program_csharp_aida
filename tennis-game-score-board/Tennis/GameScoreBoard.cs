namespace Tennis;

public class GameScoreBoard
{
    private const string PlayerName1 = "player1";
    private const string PlayerName2 = "player2";
    private readonly Reader _reader;
    private readonly Notifier _notifier;
    private readonly Game _game;

    public GameScoreBoard(Reader reader, Notifier notifier)
    {
        _reader = reader;
        _notifier = notifier;
        _game = new Game(PlayerName1, PlayerName2);
    }

    public void StartGame()
    {
        var refereeInput = ReadRefereeInput();

        if (refereeInput == "$ score 1")
        {
            ScorePlayer1();
            Print("Fifteen Love");
        }
        else
        {
            ScorePlayer2();
            Print("Love Fifteen");
        }
    }

    private void ScorePlayer1()
    {
        _game.AddPointForPlayer(PlayerName1);
    }

    private void ScorePlayer2()
    {
        _game.AddPointForPlayer(PlayerName2);
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