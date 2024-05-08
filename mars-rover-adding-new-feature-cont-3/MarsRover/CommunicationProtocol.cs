using System.Collections.Generic;
using System.Linq;
using MarsRover.communicationProtocols;

namespace MarsRover;

public abstract class CommunicationProtocol
{
    private readonly ChunkCommandExtractor _chunkCommandExtractor;

    protected CommunicationProtocol(ChunkCommandExtractor chunkCommandExtractor) {
        _chunkCommandExtractor = chunkCommandExtractor;
    }

    public List<Command> CreateCommands(string commandsSequence, int displacement) {
        var commandRepresentations = _chunkCommandExtractor.Extract(commandsSequence);
        return commandRepresentations
            .Select(commandRepresentation => CreateCommand(displacement, commandRepresentation))
            .ToList();
    }

    protected abstract Command CreateCommand(int displacement, string commandRepresentation);
}