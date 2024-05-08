using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.communicationProtocols;
using MarsRover.communicationProtocols.commandExtractor;

namespace MarsRover.Tests {
    public class JaxaCommandExtractorTest {

        //Lista de test
        // "" -> Lista vacía
        // "a" -> Lista vacía
        // "ab" -> Lista vacía
        // "at" -> ["at"]
        // "iz" -> ["iz"]
        // "abc" -> Lista vacía
        // "del" -> ["del"]
        // "der" -> ["der"]
        // "adel" -> Lista vacía
        // "atdel" -> ["at","del"]
        // "dela" -> ["del"]
        // "atdelder" -> ["at","del","der"]
        // "atdelader" -> ["at","del"]


        [Test]
        public void extract_commands_without_commands() {
            var commandExtractor = new JaxaCommandExtractor();

            var result = commandExtractor.Extract("");

            Assert.That(result, Is.EqualTo(new List<string> {}));
        }

    }
}
