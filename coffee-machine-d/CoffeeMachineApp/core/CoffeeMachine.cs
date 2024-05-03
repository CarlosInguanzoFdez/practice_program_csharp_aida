using System.Collections.Generic;

namespace CoffeeMachineApp.core;

public class CoffeeMachine
{
    private readonly DrinkMakerDriver _drinkMakerDriver;
    private readonly Dictionary<DrinkType, decimal> _prices;
    private readonly Notifier _notifier;
    private Order _order;
    private decimal _totalMoney;
    private readonly LocalPricesCatalog _pricesCatalog;

    public CoffeeMachine(DrinkMakerDriver drinkMakerDriver, Dictionary<DrinkType, decimal> prices,
        Notifier notifier)
    {
        _drinkMakerDriver = drinkMakerDriver;
        _prices = prices;
        _notifier = notifier;
        _pricesCatalog = new LocalPricesCatalog(prices);
        InitializeState();
    }

    public void SelectChocolate()
    {
        _order.SelectDrink(DrinkType.Chocolate);
    }

    public void SelectTea()
    {
        _order.SelectDrink(DrinkType.Tea);
    }

    public void SelectCoffee()
    {
        _order.SelectDrink(DrinkType.Coffee);
    }

    public void AddOneSpoonOfSugar()
    {
        _order.AddSpoonOfSugar();
    }

    public void AddMoney(decimal money)
    {
        _totalMoney += money;
    }

    public void MakeDrink()
    {
        if (NoDrinkWasSelected())
        {
            _notifier.NotifySelectDrink();
            return;
        }

        if (IsThereEnoughMoney())
        {
            _drinkMakerDriver.Send(_order);
            InitializeState();
        }
        else
        {
            _notifier.NotifyMissingPrice(ComputeMissingMoney());
        }
    }

    private void InitializeState()
    {
        _totalMoney = 0;
        _order = new Order();
    }

    private decimal ComputeMissingMoney()
    {
        return GetPrice(_order.GetDrinkType()) - _totalMoney;
    }

    private bool IsThereEnoughMoney()
    {
        return _totalMoney >= GetPrice(_order.GetDrinkType());
    }

    private decimal GetPrice(DrinkType drinkType)
    {
        return _prices[drinkType];
    }

    private bool NoDrinkWasSelected()
    {
        return _order.GetDrinkType() == DrinkType.None;
    }
}

public class LocalPricesCatalog
{
    private readonly Dictionary<DrinkType, decimal> _prices;

    public LocalPricesCatalog(Dictionary<DrinkType, decimal> prices)
    {
        _prices = prices;
    }
}
