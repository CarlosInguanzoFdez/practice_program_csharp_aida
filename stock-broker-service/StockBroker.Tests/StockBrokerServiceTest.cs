using NSubstitute;
using NUnit.Framework;

namespace StockBroker.Tests
{
    public class StockBrokerServiceTest
    {
        private Notifier _notifier;
        private StockBrokerOnlineService _stockBrokerOnlineService;

        [Test]
        public void Canary_Test()
        {
            _notifier = Substitute.For<Notifier>();
            _stockBrokerOnlineService = Substitute.For<StockBrokerOnlineService>();

            Assert.That(true, Is.True);
        }
    }
}