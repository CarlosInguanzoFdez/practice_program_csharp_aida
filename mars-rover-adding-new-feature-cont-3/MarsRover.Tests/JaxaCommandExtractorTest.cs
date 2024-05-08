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
        private CommandExtractor commandExtractor;

        //Lista de test
        // "" -> Lista vacía - Ok
        // "a" -> Lista vacía - Ok
        // "ab" -> Lista vacía - OK
        // "at" -> ["at"] - Ok
        // "iz" -> ["iz"] - Ok
        // "abc" -> Lista vacía
        // "del" -> ["del"] - Ok
        // "der" -> ["der"] - Ok
        // "adel" -> Lista vacía
        // "atdel" -> ["at","del"] - Ok
        // "dela" -> ["del"] - Ok
        // "atdelder" -> ["at","del","der"]
        // "atdelader" -> ["at","del"]
        [SetUp]
        public void SetUp() {
            commandExtractor = new JaxaCommandExtractor();
        }

        [Test]
        public void extract_from_empty_sequence() {
            var result = commandExtractor.Extract("");

            Assert.That(result, Is.EqualTo(new List<string> {}));
        }

        [TestCase("at")]
        [TestCase("iz")]
        [TestCase("del")]
        [TestCase("der")]
        public void extract_from_sequence_with_one_jaxa_command(string command) {
            var result = commandExtractor.Extract(command);

            Assert.That(result, Is.EqualTo(new List<string> { command }));
        }

        [Test]
        public void extract_from_sequence_with_not_jaxa_commands() {
            var result = commandExtractor.Extract("a");

            Assert.That(result, Is.EqualTo(new List<string> { }));
        }

        [Test]
        public void extract_from_sequence_with_one_jaxa_invalid_command()
        {
            var result = commandExtractor.Extract("ab");

            Assert.That(result, Is.EqualTo(new List<string> { }));
        }

        [Test]
        public void extract_from_sequence_with_two_jaxa_commands()
        {
            var result = commandExtractor.Extract("atdel");

            Assert.That(result, Is.EqualTo(new List<string> { "at", "del"}));
        }

        [Test]
        public void extract_from_sequence_with_one_jaxa_command_and_trash()
        {
            var result = commandExtractor.Extract("dela");

            Assert.That(result, Is.EqualTo(new List<string> { "del"}));
        }


    }
}
