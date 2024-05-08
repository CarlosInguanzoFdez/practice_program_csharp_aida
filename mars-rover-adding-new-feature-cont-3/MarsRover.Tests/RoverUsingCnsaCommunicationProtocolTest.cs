using MarsRover.Tests.helpers;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingCnsaCommunicationProtocolTest
{
    [Test]
    public void No_Commands()
    {
        var rover = GetRoverBuilder().Build();

        rover.Receive("");

        Assert.That(rover, Is.EqualTo(CsnaRover().Build()));
    }

    [Test]
    public void Forward_Commands()
    {
        var rover = CsnaRover().WithCoordinates(0, 0).Facing("N").Build();

        rover.Receive(GetForwardCommandRepresentation());

        Assert.That(rover, Is.EqualTo(GetRoverBuilder().WithCoordinates(0, 1).Facing("N").Build()));
    }

    [Test]
    public void Backward_Commands()
    {
        var rover = GetRoverBuilder().WithCoordinates(3, 3).Facing("E").Build();

        rover.Receive(GetBackwardCommandRepresentation());

        Assert.That(rover, Is.EqualTo(GetRoverBuilder().WithCoordinates(2, 3).Facing("E").Build()));
    }

    [Test]
    public void Rotate_Left_Commands()
    {
        var rover = GetRoverBuilder().Facing("E").Build();

        rover.Receive(GetRotateLeftCommandRepresentation());

        Assert.That(rover, Is.EqualTo(GetRoverBuilder().Facing("N").Build()));
    }

    [Test]
    public void Rotate_Right_Commands()
    {
        var rover = GetRoverBuilder().Facing("W").Build();

        rover.Receive(GetRotateRightCommandRepresentation());

        Assert.That(rover, Is.EqualTo(GetRoverBuilder().Facing("N").Build()));
    }

    [Test]
    public void Two_Commands()
    {
        var rover = GetRoverBuilder().Facing("W").Build();

        rover.Receive("ahpl");

        Assert.That(rover, Is.EqualTo(GetRoverBuilder().Facing("W").Build()));
    }

    private RoverBuilder GetRoverBuilder()
    {
        return CsnaRover();
    }

    private string GetForwardCommandRepresentation()
    {
        return "bx";
    }

    private string GetBackwardCommandRepresentation()
    {
        return "tf";
    }

    private string GetRotateRightCommandRepresentation()
    {
        return "pl";
    }

    private string GetRotateLeftCommandRepresentation()
    {
        return "ah";
    }
}