namespace Tennis;

public class GameScoreBoard
{
    private readonly Reader _reader;
    private readonly Notifier _notifier;
    private readonly Game _game;

    public GameScoreBoard(Reader reader, Notifier notifier)
    {
        _reader = reader;
        _notifier = notifier;
        _game = new Game();
    }

    public void StartGame()
    {
        while (true)
        {
            var refereeInput = ReadRefereeInput();

            if (refereeInput == "$ score 1") 
            {
                _game.AddPointForPlayer1();
            }
            else
            {
                _game.AddPointForPlayer2();
            }

            Print(_game.GetScore());

            if (_game.IsFinish())
            {
                return;
            }
        }
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