using MarsRover.Tests.helpers;

namespace MarsRover.Tests;

public abstract class RoverUsingCommunicationProtocolTest
{
    protected abstract RoverBuilder GetRoverBuilder();
    protected abstract string GetForwardCommandRepresentation();
    protected abstract string GetBackwardCommandRepresentation();
    protected abstract string GetRotateRightCommandRepresentation();
    protected abstract string GetRotateLeftCommandRepresentation();
}