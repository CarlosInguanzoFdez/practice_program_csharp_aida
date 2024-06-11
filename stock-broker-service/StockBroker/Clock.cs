using System;

namespace StockBroker;

public interface Clock
{
    DateTime Get();
}