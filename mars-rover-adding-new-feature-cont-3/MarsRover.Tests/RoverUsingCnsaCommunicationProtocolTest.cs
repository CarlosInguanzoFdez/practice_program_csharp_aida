using MarsRover.Tests.helpers;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingCnsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    [Test]
    public void Two_Commands()
    {
        var rover = GetRoverBuilder().Facing("W").Build();

        rover.Receive("ahpl");

        Assert.That(rover, Is.EqualTo(GetRoverBuilder().Facing("W").Build()));
    }

    protected override RoverBuilder GetRoverBuilder()
    {
        return CsnaRover();
    }

    protected override string GetForwardCommandRepresentation()
    {
        return "bx";
    }

    protected override string GetBackwardCommandRepresentation()
    {
        return "tf";
    }

    protected override string GetRotateRightCommandRepresentation()
    {
        return "pl";
    }

    protected override string GetRotateLeftCommandRepresentation()
    {
        return "ah";
    }
}