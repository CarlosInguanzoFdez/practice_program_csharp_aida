using MarsRover.commands;

namespace MarsRover.communicationProtocols;

public class NasaCommunicationProtocol : CommunicationProtocol {
    public NasaCommunicationProtocol() : base(new CommandExtractor(1)) {

    }

    protected override Command CreateCommand(int displacement, string commandRepresentation) {
        switch (commandRepresentation) {
            case "l":
                return new RotationLeft();
            case "r":
                return new RotationRight();
            case "f":
                return new MovementForward(displacement);
            default:
                return new MovementBackward(displacement);
        }
    }
}