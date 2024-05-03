using NUnit.Framework;

namespace MarsRover.Tests;

public class RoverEqualityTest
{
    [Test]
    public void Equal_Rovers()
    {
        Assert.That(new Rover(1, 1, "N"), Is.EqualTo(new Rover(1, 1, "N")));
    }

    [Test]
    public void Not_Equal_Rovers()
    {
        Assert.That(new Rover(1, 1, "N"), Is.Not.EqualTo(new Rover(1, 1, "S")));
        Assert.That(new Rover(1, 1, "N"), Is.Not.EqualTo(new Rover(1, 2, "N")));
        Assert.That(new Rover(1, 1, "N"), Is.Not.EqualTo(new Rover(0, 1, "N")));
    }
}

public class RoverBuilder {
    private string _direction;
    private int _x;
    private int _y;

    public static RoverBuilder ANasaRover() {
        return new RoverBuilder();
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

    public RoverBuilder() {
        
    }

    
}