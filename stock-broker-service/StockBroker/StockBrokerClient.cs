using System;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient
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
        var orderDto = CreateOrderDto(orderSequence);

        if (!string.IsNullOrEmpty(orderSequence)){
            _stockBrokerOnlineService.Buy(orderDto);
        }
        _notifier.Notify(GetFormatMessage(dateTimeOrder, orderDto));
    }

    private string GetFormatMessage(DateTime dateTimeOrder, OrderDto orderDto)
    {
        var dateTimerOrderFormated = dateTimeOrder.ToString(StandarFormatCode, _currentCultureInfo);

        return $"{dateTimerOrderFormated} Buy: \u20ac {orderDto.price.ToString(StandarFormatCode, _currentCultureInfo)}, Sell: \u20ac 0.00";
    }

    private OrderDto CreateOrderDto(string orderSequence)
    {
        if (string.IsNullOrEmpty(orderSequence))
        {
            return new OrderDto("", 0, 0.00m);
        }

        var paramsOrder = orderSequence.Split(" ");
        var price = decimal.Parse(paramsOrder[2], _currentCultureInfo);
        var orderDto = new OrderDto(paramsOrder[0], int.Parse(paramsOrder[1]), price);
        return orderDto;
    }
}