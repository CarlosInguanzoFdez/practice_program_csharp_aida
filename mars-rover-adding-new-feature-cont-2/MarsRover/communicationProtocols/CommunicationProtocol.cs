using System.Collections.Generic;
using System.Linq;

namespace MarsRover.communicationProtocols;

public abstract class CommunicationProtocol
{
    private readonly CommandExtractor _commandExtractor;

    protected CommunicationProtocol(CommandExtractor commandExtractor) {
        _commandExtractor = commandExtractor;
    }

    public List<Command> CreateCommands(string commandsSequence, int displacement) {
        var commandRepresentations = _commandExtractor.Extract(commandsSequence);
        return commandRepresentations
            .Select(commandRepresentation => CreateCommand(displacement, commandRepresentation))
            .ToList();
    }

    protected abstract Command CreateCommand(int displacement, string commandRepresentation);
}