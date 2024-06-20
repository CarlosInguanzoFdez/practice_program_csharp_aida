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
        _player1._points++;
    }

    public void AddPointForPlayer2()
    {
        _player2._points++;
    }


    public string GetScore()
    {
        if (_player1._points == 1 && _player2._points == 0) {
            return "Fifteen Love";
        }

        if (_player1._points == 0 && _player2._points == 1)
        {
            return "Love Fifteen";
        }

        if (_player1._points == 2 && _player2._points == 0)
        {
            return "Thirty Love";
        }

        return "xxx";
    }
}