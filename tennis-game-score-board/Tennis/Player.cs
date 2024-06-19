namespace Tennis;

public class Player
{
    public readonly string _name;
    public int _points;

    public Player(string name)
    {
        _name = name;
        _points = 0;
    }
}