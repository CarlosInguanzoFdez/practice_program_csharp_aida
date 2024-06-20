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

    public bool Win()
    {
        return Points > 3;
    }
}