using MarsRover.commands;

namespace MarsRover.communicationProtocols;

public class EsaCommunicationProtocol : CommunicationProtocol
{
    protected override Command CreateCommand(int displacement, string commandRepresentation)
    {
        Command command;
        if (commandRepresentation == "b")
            command = new MovementForward(displacement);
        else if (commandRepresentation == "x")
            command = new MovementBackward(displacement);

        else if (commandRepresentation =="f")
            command = new RotationLeft();
        else
            command = new RotationRight();

        return command;
    }
}