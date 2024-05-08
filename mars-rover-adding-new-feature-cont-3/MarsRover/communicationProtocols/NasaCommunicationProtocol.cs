using MarsRover.commands;

namespace MarsRover.communicationProtocols;

public class NasaCommunicationProtocol : CommunicationProtocol
{
    public NasaCommunicationProtocol() : base(new ChunkCommandExtractor(1))
    {
    }

    public NasaCommunicationProtocol(CommandExtractor commandExtractor) : base(commandExtractor)
    {
        
    }

    protected override Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "l" => new RotationLeft(),
            "r" => new RotationRight(),
            "f" => new MovementForward(displacement),
            _ => new MovementBackward(displacement)
        };
    }
}