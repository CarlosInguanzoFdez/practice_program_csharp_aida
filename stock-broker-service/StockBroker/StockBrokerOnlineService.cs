using System.Collections.Concurrent;

namespace StockBroker;

public interface StockBrokerOnlineService
{
    void Buy(StockOrderDto stockOrderDto);
    
    void Sell(StockOrderDto stockOrderDto);
}