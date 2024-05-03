namespace MarsRover.Tests;

public class RoverBuilder {
    private string _direction;
    private int _x;
    private int _y;

    public static RoverBuilder ANasaRover() {
        return new RoverBuilder(0, 0, "N");
    }

    public RoverBuilder Facing(string direction) {
        _direction = direction;
        return this;
    }

    public RoverBuilder WithX(int x) {
        _x = x;
        return this;
    }

    public RoverBuilder WithY(int y) {
        _y = y;
        return this;
    }

    public Rover Build() {
        return new Rover(_x, _y, _direction);
    }

    private RoverBuilder(int x, int y, string direction) {
        _direction = direction;
        _x = x;
        _y = y;
    }

    
}