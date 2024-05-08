using System;
using System.Collections.Generic;

namespace MarsRover.communicationProtocols.commandExtractor;

public class JaxaCommandExtractor : CommandExtractor {
    public List<string> Extract(string commandsSequence) {
        if (commandsSequence.Length < 2) {
            return new List<string>();
        }

        return new List<string> { "at" };
    }
}