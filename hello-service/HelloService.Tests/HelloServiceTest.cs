using ApprovalUtilities.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace Hello.Tests
{
    public class HelloServiceTest
    {
        private Notifier _notify;
        private Clock _clock;
        private ServiceHello _serviceHello;

        [SetUp]
        public void Setup()
        {
            _notify = Substitute.For<Notifier>();
            _clock = Substitute.For<Clock>();
            _serviceHello = new ServiceHello(_notify, _clock);
        }

        [Test]
        public void When_between_6AM_and_12AM_return_buenos_dias()
        {
            _clock.Get().Returns(new DateTime(2024,6,11,7,0,0));

            _serviceHello.Hello();

            _notify.Received(1).Notify("Buenos días");
        }

        [Test]
        public void When_between_12AM_and_8PM_return_buenas_tardes()
        {
            _clock.Get().Returns(new DateTime(2024, 6, 11, 13, 0, 0));

            _serviceHello.Hello();

            _notify.Received(1).Notify("Buenas tardes");
        }
    }
}