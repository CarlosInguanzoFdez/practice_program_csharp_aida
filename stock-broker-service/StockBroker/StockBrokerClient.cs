using System;
using System.Globalization;

namespace StockBroker;

public partial class StockBrokerClient
{
    private const string StandarFormatCode = "g";
    private readonly Notifier _notifier;
    private readonly Clock _clock;
    private readonly StockBrokerOnlineService _stockBrokerOnlineService;
    private CultureInfo _currentCultureInfo;

    public StockBrokerClient(Notifier notifier, Clock clock, StockBrokerOnlineService stockBrokerOnlineService)
    {
        _notifier = notifier;
        _clock = clock;
        _stockBrokerOnlineService = stockBrokerOnlineService;
        _currentCultureInfo = new CultureInfo("en-US");
    }

    public void PlaceOrders(string orderSequence)
    {
        var dateTimeOrder = _clock.Get();
        var order = CreateOrder(orderSequence);

        if (!string.IsNullOrEmpty(orderSequence)){
            if (order.orderType == "B"){
                _stockBrokerOnlineService.Buy(CreateStockOrderDto(order));
            }
            else {
                _stockBrokerOnlineService.Sell(CreateStockOrderDto(order));
            }
        }

        _notifier.Notify(GetFormatMessage(dateTimeOrder, order));
    }

    private string GetFormatMessage(DateTime dateTimeOrder, Order order)
    {
        var dateTimerOrderFormated = dateTimeOrder.ToString(StandarFormatCode, _currentCultureInfo);
        var totalBuy = order.price * order.quantity;
        if (order.orderType == "B"){
            return $"{dateTimerOrderFormated} Buy: \u20ac {totalBuy.ToString(StandarFormatCode, _currentCultureInfo)}, Sell: \u20ac 0.00";
        }
        return $"{dateTimerOrderFormated} Buy: \u20ac 0.00, Sell: \u20ac {totalBuy.ToString(StandarFormatCode, _currentCultureInfo)}";
    }

    private StockOrderDto CreateStockOrderDto(Order order)
    {
        var stockOrderDto = new StockOrderDto(order.ticker, order.quantity, order.price);
        return stockOrderDto;
    }

    private Order CreateOrder(string orderSequence)
    {
        if (string.IsNullOrEmpty(orderSequence))
        {
            return new Order("", 0, 0.00m, "");
        }

        var paramsOrder = orderSequence.Split(" ");
        var price = decimal.Parse(paramsOrder[2], _currentCultureInfo);
        var order = new Order(paramsOrder[0], int.Parse(paramsOrder[1]), price, paramsOrder[3]);
        return order;
    }
}