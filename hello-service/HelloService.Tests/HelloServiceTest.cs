using NSubstitute;
using NUnit.Framework;

namespace Hello.Tests
{
    public class HelloServiceTest
    {
        private Notifier _notify;

        [Test]
        public void Canary_Test()
        {
            _notify = Substitute.For<Notifier>();
            _clock = Substitute.For<Clock>();
        }
    }
}