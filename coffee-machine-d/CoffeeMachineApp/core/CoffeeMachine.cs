using System.Collections.Generic;

namespace CoffeeMachineApp.core;

public class CoffeeMachine
{
    private readonly DrinkMakerDriver _drinkMakerDriver;
    private readonly Notifier _notifier;
    private Order _order;
    private decimal _totalMoney;
    private readonly PricesCatalog _pricesCatalog;

    public CoffeeMachine(DrinkMakerDriver drinkMakerDriver, Notifier notifier, PricesCatalog pricesCatalog)
    {
        _drinkMakerDriver = drinkMakerDriver;
        _notifier = notifier;
        _pricesCatalog = pricesCatalog;
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
        return _pricesCatalog.GetPrice(_order.GetDrinkType()) - _totalMoney;
    }

    private bool IsThereEnoughMoney()
    {
        return _totalMoney >= _pricesCatalog.GetPrice(_order.GetDrinkType());
    }

    private bool NoDrinkWasSelected()
    {
        return _order.GetDrinkType() == DrinkType.None;
    }
}