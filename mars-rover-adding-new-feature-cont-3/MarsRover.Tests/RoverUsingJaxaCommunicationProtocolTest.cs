using MarsRover.Tests.helpers;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests
{
    public class RoverUsingJaxaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
    {

        [Test]
        public void Two_Commands() {
            var rover = GetRoverBuilder().Facing("W").WithCoordinates(5,5).Build();

            rover.Receive("derat");

            Assert.That(rover, Is.EqualTo(GetRoverBuilder().Facing("N").WithCoordinates(5,4).Build()));
        }

        protected override RoverBuilder GetRoverBuilder() {
            return JaxaRover();
        }

        protected override string GetForwardCommandRepresentation() {
            return "del";
        }

        protected override string GetBackwardCommandRepresentation() {
            return "at";
        }

        protected override string GetRotateRightCommandRepresentation() {
            return "der";
        }

        protected override string GetRotateLeftCommandRepresentation() {
            return "iz";
        }
    }
}
