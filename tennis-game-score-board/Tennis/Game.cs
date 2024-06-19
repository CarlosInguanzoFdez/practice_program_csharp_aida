namespace Tennis;

public class Game
{
    private readonly Player _player1;
    private readonly Player _player2;

    public Game(string player1, string player2)
    {
        _player1 = new Player(player1);
        _player2 = new Player(player2);
    }

    public void AddPointForPlayer(string player)
    {
        if (player == _player1._name) {
            _player1._points++;
            return;
        }

        _player2._points++;
    }
}