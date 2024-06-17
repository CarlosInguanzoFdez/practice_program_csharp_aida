using System.Collections.Generic;
using System.Globalization;
using static StockBroker.StockBrokerClient;

namespace StockBroker;

public partial class StockBrokerClient
{
    private readonly Notifier _notifier;
    private readonly StockBrokerOnlineService _stockBrokerOnlineService;
    private readonly OrderParser _orderParser;
    private readonly OperationsSummaryFormatter _operationsSummaryFormatter;

    public StockBrokerClient(Notifier notifier, Clock clock, StockBrokerOnlineService stockBrokerOnlineService)
    {
        var currentCultureInfo = new CultureInfo("en-US");
        _notifier = notifier;
        _stockBrokerOnlineService = stockBrokerOnlineService;
        _orderParser = new OrderParser(currentCultureInfo);
        _operationsSummaryFormatter = new OperationsSummaryFormatter(currentCultureInfo, clock);
    }

    public void PlaceOrders(string orderSequence)
    {
        var orderList = _orderParser.MultipleParse(orderSequence);

        foreach (var order in orderList) {
            if (order.isBuy)
            {
                _stockBrokerOnlineService.Buy(CreateStockOrderDto(order));
            }
            else
            {
                _stockBrokerOnlineService.Sell(CreateStockOrderDto(order));
            }
        }

        _notifier.Notify(GetFormatMessage(orderList));
    }

    private string GetFormatMessage(List<Order> orderList)
    {
        return _operationsSummaryFormatter.FormatMessage(orderList);
    }

    private StockOrderDto CreateStockOrderDto(Order order)
    {
        var stockOrderDto = new StockOrderDto(order.ticker, order.quantity);
        return stockOrderDto;
    }
}