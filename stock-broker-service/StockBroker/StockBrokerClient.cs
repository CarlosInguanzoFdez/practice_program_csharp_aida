namespace StockBroker;

public class StockBrokerClient
{
    private readonly Notifier _notifier;
    private readonly Clock _clock;
    private readonly StockBrokerOnlineService _stockBrokerOnlineService;

    public StockBrokerClient(Notifier notifier, Clock clock, StockBrokerOnlineService stockBrokerOnlineService)
    {
        _notifier = notifier;
        _clock = clock;
        _stockBrokerOnlineService = stockBrokerOnlineService;
    }

    public void PlaceOrders(string orderSequence)
    {
        _notifier.Notify("6/11/2024 1:45 PM Buy: \u20ac 0.00, Sell: \u20ac 0.00");
    }
}