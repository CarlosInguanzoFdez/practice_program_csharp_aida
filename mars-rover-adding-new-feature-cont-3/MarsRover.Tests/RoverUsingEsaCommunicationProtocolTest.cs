using MarsRover.Tests.helpers;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingEsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    
    [Test]
    public void Two_Commands()
    {
        var rover = GetRoverBuilder().Facing("W").Build();

        rover.Receive("lf");

        Assert.That(rover, Is.EqualTo(GetRoverBuilder().Facing("W").Build()));
    }

    protected override RoverBuilder GetRoverBuilder()
    {
        return EsaRover();
    }

    protected override string GetForwardCommandRepresentation()
    {
        return "b";
    }

    protected override string GetBackwardCommandRepresentation()
    {
        return "x";
    }

    protected override string GetRotateRightCommandRepresentation()
    {
        return "l";
    }

    protected override string GetRotateLeftCommandRepresentation()
    {
        return "f";
    }
}