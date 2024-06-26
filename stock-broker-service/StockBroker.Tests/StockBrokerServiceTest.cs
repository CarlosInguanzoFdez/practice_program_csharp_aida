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
            _clock = Substitute.For<Clock>();
            _stockBrokerOnlineService = Substitute.For<StockBrokerOnlineService>();
            _stockBrokerClient = new StockBrokerClient(_notifier, _clock, _stockBrokerOnlineService);
        }

        [Test]
        public void place_a_empty_order_should_show_the_date_and_summary_empty()
        {
            _clock.Get().Returns(new DateTime(2024, 06, 11, 13, 45, 00));

            _stockBrokerClient.PlaceOrders("");

            VerifyNotifierIsCalledWith("6/11/2024 1:45 PM Buy: \u20ac 0.00, Sell: \u20ac 0.00");
            _stockBrokerOnlineService.DidNotReceive().Buy(Arg.Any<StockOrderDto>());
        }

        [Test]
        public void buy_order_with_one_product()
        {
            _clock.Get().Returns(new DateTime(2023,10,20,13, 45,00));
            
            _stockBrokerClient.PlaceOrders("GOOG 1 200.00 B");

            VerifyNotifierIsCalledWith("10/20/2023 1:45 PM Buy: \u20ac 200.00, Sell: \u20ac 0.00");
            _stockBrokerOnlineService.Received(1).Buy(new StockOrderDto("GOOG", 1));
        }

        [Test]
        public void sell_order_with_one_product()
        {
            _clock.Get().Returns(new DateTime(2023, 10, 20, 13, 45, 00));

            _stockBrokerClient.PlaceOrders("GOOG 3 150.00 S");

            VerifyNotifierIsCalledWith("10/20/2023 1:45 PM Buy: \u20ac 0.00, Sell: \u20ac 450.00");
            _stockBrokerOnlineService.DidNotReceive().Buy(Arg.Any<StockOrderDto>());
            _stockBrokerOnlineService.Received(1).Sell(new StockOrderDto("GOOG", 3));
        }

        [Test]
        public void order_with_one_product_with_several_quantities()
        {
            _clock.Get().Returns(new DateTime(2023, 10, 25, 14, 45, 00));

            _stockBrokerClient.PlaceOrders("GOOG 2 200.00 B");

            VerifyNotifierIsCalledWith("10/25/2023 2:45 PM Buy: \u20ac 400.00, Sell: \u20ac 0.00");
            _stockBrokerOnlineService.Received(1).Buy(new StockOrderDto("GOOG", 2));
        }

        [Test]
        public void order_with_buy_and_sell()
        {
            _clock.Get().Returns(new DateTime(2023, 10, 25, 14, 45, 00));

            _stockBrokerClient.PlaceOrders("ZNGA 3 100 B,AAPL 2 300 S");

            VerifyNotifierIsCalledWith("10/25/2023 2:45 PM Buy: \u20ac 300.00, Sell: \u20ac 600.00");
            _stockBrokerOnlineService.Received(1).Buy(Arg.Any<StockOrderDto>());
            _stockBrokerOnlineService.Received(1).Sell(Arg.Any<StockOrderDto>());
            _stockBrokerOnlineService.Received(1).Buy(new StockOrderDto("ZNGA", 3));
            _stockBrokerOnlineService.Received(1).Sell(new StockOrderDto("AAPL", 2));
        }

        [Ignore("")]
        [Test]
        public void call_buy_online_service_failed()
        {
            _clock.Get().Returns(new DateTime(2023, 10, 25, 14, 45, 00));
            _stockBrokerOnlineService.Buy(Arg.Any<StockOrderDto>());

            _stockBrokerClient.PlaceOrders("ZNGA 3 100 B,AAPL 2 300 S");

            VerifyNotifierIsCalledWith("10/25/2023 2:45 PM Buy: \u20ac 300.00, Sell: \u20ac 600.00");
        }

        private void VerifyNotifierIsCalledWith(string message)
        {
            _notifier.Received(1).Notify(message);
            _notifier.Received(1).Notify(Arg.Any<string>());
        }
    }
}