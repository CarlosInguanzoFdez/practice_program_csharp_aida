using System.Collections.Generic;
using System.Linq;

namespace MarsRover;

public class CommandExtractor {
    private readonly uint _length;

    public CommandExtractor(uint length) {
        _length = length;
    }

    public List<string> Extract(string commandsSequence) {
        return commandsSequence
            .Chunk((int)_length)
            .Select(arr => arr[0].ToString()).ToList();
    }
}