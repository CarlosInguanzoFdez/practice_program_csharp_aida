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
        public void place_a_empty_order_should_show_the_date_and_summary_empty()
        {
            _clock.Get().Returns(new DateTime(2024, 06, 11, 13, 45, 00));

            _stockBrokerClient.PlaceOrders("");

            VerifyNotifierIsCalledWith("6/11/2024 1:45 PM Buy: \u20ac 0.00, Sell: \u20ac 0.00");
            _stockBrokerOnlineService.DidNotReceive().Buy(Arg.Any<OrderDto>());
        }

        [Test]
        public void buy_order_with_one_quantity()
        {
            _clock.Get().Returns(new DateTime(2024,06,11,13, 45,00));
            
            _stockBrokerClient.PlaceOrders("GOOG 1 200.00 B");

            VerifyNotifierIsCalledWith("6/11/2024 1:45 PM Buy: \u20ac 200.00, Sell: \u20ac 0.00");
            _stockBrokerOnlineService.Received(1).Buy(new OrderDto("GOOG", 1, 200.00m));
        }
        private void VerifyNotifierIsCalledWith(string message)
        {
            _notifier.Received(1).Notify(message);
            _notifier.Received(1).Notify(Arg.Any<string>());
        }

        /*
            ejemplo vacio: DONE
            ejemplo buy con un producto y 1 quantity: DONE

            ejemplo sell con un producto y 1 quantity
            ejemplo con varios productos y 1 quantity cada uno
            ejemplo con varios productos y varios quantity cada uno
            ejemplo error con un producto
            ejemplo error con varios productos
         */
    }
}