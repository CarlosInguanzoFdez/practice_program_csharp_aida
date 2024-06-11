using NSubstitute;
using NUnit.Framework;

namespace StockBroker.Tests
{
    public class StockBrokerServiceTest
    {
        private Notifier _notifier;
        private StockBrokerOnlineService _stockBrokerOnlineService;
        private Clock _clock;
        private StockBrokerClient _stockBrokerClient;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<Notifier>();
            _stockBrokerOnlineService = Substitute.For<StockBrokerOnlineService>();
            _clock = Substitute.For<Clock>();
            _stockBrokerClient = new StockBrokerClient(_notifier, _clock, _stockBrokerOnlineService);
        }

        [Test]
        public void place_a_empty_order()
        {
            _clock.Get().Returns(new DateTime(2024, 06, 11, 13, 45, 00));

            _stockBrokerClient.PlaceOrders("");

            _notifier.Received(1).Notify("6/11/2024 1:45 PM Buy: \u20ac 0.00, Sell: \u20ac 0.00");
            _notifier.Received(1).Notify(Arg.Any<string>());
        }

        [Ignore("")]
        [Test]
        public void buy_order_with_one_quantity()
        {
            _clock.Get().Returns(new DateTime(2024,06,11,13, 45,00));
            
            _stockBrokerClient.PlaceOrders("GOOG 1 500.00 B");

            _notifier.Received(1).Notify("6/11/2024 1:45 PM Buy: 0.00, Sell: 0.00, Failed: GOOG");
            _notifier.Received(1).Notify(Arg.Any<string>());
        }
    }
}