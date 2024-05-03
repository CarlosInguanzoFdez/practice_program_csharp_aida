using System.Collections.Generic;
using CoffeeMachineApp.core;

namespace CoffeeMachineApp.infrastructure.pricecatalog;

public class LocalPricesCatalog : PricesCatalog
{
    private readonly Dictionary<DrinkType, decimal> _prices;

    public LocalPricesCatalog(Dictionary<DrinkType, decimal> prices)
    {
        _prices = prices;
    }

    public decimal GetPrice(DrinkType drinkType)
    {
        return _prices[drinkType];
    }
}