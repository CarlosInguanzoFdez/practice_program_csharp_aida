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
        [TestCase("06:00:00 AM")]
        [TestCase("11:59:59 AM")]
        public void When_between_6AM_and_12AM_return_buenos_dias(string time)
        {
            _clock.Get().Returns(CreateDateTime(time));

            _serviceHello.Hello();

            _notify.Received(1).Notify("Buenos días");
            _notify.Received(1).Notify(Arg.Any<string>());
        }

        [Test]
        [TestCase("12:00:00 PM")]
        [TestCase("07:59:59 PM")]
        public void When_between_12AM_and_8PM_return_buenas_tardes(string time)
        {
            _clock.Get().Returns(CreateDateTime(time));

            _serviceHello.Hello();

            _notify.Received(1).Notify("Buenas tardes");
            _notify.Received(1).Notify(Arg.Any<string>());
        }

        [Test]
        [TestCase("08:00:00 PM")]
        [TestCase("05:59:59 AM")]
        public void When_not_between_6AM_and_12AM_and_12AM_and_8PM_return_buenas_noches(string time)
        {
            _clock.Get().Returns(CreateDateTime(time));

            _serviceHello.Hello();

            _notify.Received(1).Notify("Buenas noches");
            _notify.Received(1).Notify(Arg.Any<string>());
        }

        private DateTime CreateDateTime(string time)
        {
            return DateTime.Parse("11/6/2024 " + time);
        }
    }
}