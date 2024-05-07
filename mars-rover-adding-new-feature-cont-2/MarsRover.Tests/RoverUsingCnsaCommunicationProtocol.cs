using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Tests
{
    public class RoverUsingCnsaCommunicationProtocol
    {
        [Test]
        public void No_Commands()
        {
            var rover = RoverBuilder.ACNSARover().Build();

            rover.Receive("");

            Assert.That(rover, Is.EqualTo(rover));
        }
    }
}
