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
        var order = _orderParser.Parse(orderSequence);

        if (!string.IsNullOrEmpty(orderSequence)){
            if (order.isBuy){
                _stockBrokerOnlineService.Buy(CreateStockOrderDto(order));
            }
            else {
                _stockBrokerOnlineService.Sell(CreateStockOrderDto(order));
            }
        }

        _notifier.Notify(GetFormatMessage(order));
    }

    private string GetFormatMessage(Order order)
    {
        return _operationsSummaryFormatter.FormatMessage(order);
    }

    private StockOrderDto CreateStockOrderDto(Order order)
    {
        var stockOrderDto = new StockOrderDto(order.ticker, order.quantity);
        return stockOrderDto;
    }
}