using System.Collections.Generic;
using System.Linq;

namespace MarsRover;

public abstract class CommunicationProtocol
{
    public List<Command> CreateCommands(string commandsSequence, int displacement)
    {
        return commandsSequence
            .Select(char.ToString)
            .Select(commandRepresentation => CreateCommand(displacement, commandRepresentation))
            .ToList();
    }

    protected abstract Command CreateCommand(int displacement, string commandRepresentation);
}