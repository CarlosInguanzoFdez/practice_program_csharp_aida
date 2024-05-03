using NUnit.Framework;
using static MarsRover.Tests.RoverBuilder;

namespace MarsRover.Tests;

public class RoverEqualityTest
{
    [Test]
    public void Equal_Rovers()
    {
        Assert.That(ANasaRover().Build(), Is.EqualTo(ANasaRover().Build()));
    }

    [Test]
    public void Not_Equal_Rovers()
    {
        Assert.That(ANasaRover().Facing("N").Build(), Is.Not.EqualTo(ANasaRover().Facing("S").Build()));
        Assert.That(ANasaRover().WithY(10).Build(), Is.Not.EqualTo(ANasaRover().WithY(2).Build()));
        Assert.That(ANasaRover().WithX(8).Build(), Is.Not.EqualTo(ANasaRover().WithX(7).Build()));
    }
}