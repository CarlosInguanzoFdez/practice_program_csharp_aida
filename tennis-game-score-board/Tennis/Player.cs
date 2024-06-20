namespace Tennis;

public class Player
{
    public int _points;

    public Player()
    {
        _points = 0;
    }
    public int Points => _points;

    public int PointWon()
    {
        return _points++;
    }

    public bool Win(Player otherPlayer)
    {
        return Points > 3 && Points > otherPlayer.Points + 1;
    }

    public bool Deuce(Player otherPlayer)
    {
        return Points >= 3 && Points == otherPlayer.Points;
    }

    public bool HasAdvantage(Player otherPlayer)
    {
        return otherPlayer.Points >= 3 && Points > otherPlayer.Points;
    }
}