using System.Collections.Concurrent;

namespace StockBroker;

public interface StockBrokerOnlineService
{
    void Buy(OrderDto orderDto);
    
    void Sell(OrderDto orderDto);
}