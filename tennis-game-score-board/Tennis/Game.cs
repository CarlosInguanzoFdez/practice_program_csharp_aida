namespace Tennis;

public class Game
{
    private readonly Player _player1;
    private readonly Player _player2;

    public Game()
    {
        _player1 = new Player();
        _player2 = new Player();
    }

    public void AddPointForPlayer1()
    {
        _player1.PointWon();
    }

    public void AddPointForPlayer2()
    {
        _player2.PointWon();
    }

    public string GetScore()
    {
        if (_player1.Win(_player2))
        {
            return MessageForWinner("Player 1");
        }

        if (_player2.Win(_player1))
        {
            return MessageForWinner("Player 2");
        }

        if (_player1.Deuce(_player2))
        {
            return "Deuce";
        }

        if (_player1.HasAdvantage(_player2))
        {
            return "Advantage Forty";
        }

        return CurrentScore();
    }

    public bool IsFinish()
    {
        return _player1.Win(_player2) || _player2.Win(_player1);
    }

    private string CurrentScore()
    {
        return $"{TranslateToPointDescription(_player1.Points)} {TranslateToPointDescription(_player2.Points)}";
    }

    private string MessageForWinner(string player)
    {
        return $"{player} has won!!\nIt was a nice game.\nBye now!";
    }

    private string TranslateToPointDescription(int point)
    {
        if (point == 0)
        {
            return "Love";
        }
        
        if (point == 1)
        {
            return "Fifteen";
        }
        
        if (point == 2)
        {
            return "Thirty";
        }

        return "Forty";
    }
}