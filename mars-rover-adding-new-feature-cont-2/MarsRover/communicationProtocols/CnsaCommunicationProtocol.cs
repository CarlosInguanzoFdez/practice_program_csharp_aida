namespace MarsRover.communicationProtocols;

public class CnsaCommunicationProtocol : CommunicationProtocol
{
    public CnsaCommunicationProtocol() : base(new CommandExtractor(2))
    {
    }

    protected override Command CreateCommand(int displacement, string commandRepresentation)
    {
        throw new System.NotImplementedException();
    }
}