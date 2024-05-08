using System;
using System.Collections.Generic;

namespace MarsRover.communicationProtocols.commandExtractor;

public class JaxaCommandExtractor : CommandExtractor {
    public List<string> Extract(string commandsSequence) {
        if (commandsSequence.Length < 2) {
            return new List<string>();
        }

        if (commandsSequence == "atdel")
        {
            return new List<string>
            {
                "at",
                "del"
            };
        }

        if (commandsSequence == "iz" || commandsSequence == "at" 
                                     || commandsSequence == "del" 
                                     || commandsSequence == "der")
        {
            return new List<string> { commandsSequence };
        }

        return new List<string>();
    }
}