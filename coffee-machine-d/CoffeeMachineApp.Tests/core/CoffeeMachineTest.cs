using System.Collections.Generic;
using System.Linq;
using CoffeeMachineApp.core;
using CoffeeMachineApp.infrastructure.pricecatalog;
using NSubstitute;
using NUnit.Framework;
using static CoffeeMachineApp.Tests.helpers.OrderBuilder;

namespace CoffeeMachineApp.Tests.core;

public class CoffeeMachineTest
{
    private const decimal TeaPrice = 0.4m;
    private const decimal CoffeePrice = 0.6m;
    private const decimal ChocolatePrice = 0.5m;
    private CoffeeMachine _coffeeMachine;
    private DrinkMakerDriver _drinkMakerDriver;
    private Notifier _notifier;

    [SetUp]
    public void SetUp()
    {
        _drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
        _notifier = Substitute.For<Notifier>();
    }

    [Test]
    public void Make_Chocolate()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().Build());
    }

    [Test]
    public void Make_Tea()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Tea().Build());
    }

    [Test]
    public void Make_Coffee()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectCoffee();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Coffee().Build());
    }

    [Test]
    public void Make_Any_Drink_With_1_Spoon_Of_Sugar()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(1).Build());
    }

    [Test]
    public void Make_Any_Drink_With_2_Spoons_Of_Sugar()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Make_Any_Drink_With_More_Than_2_Spoons_Of_Sugar()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Warns_The_User_When_No_Drink_Was_Selected()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.MakeDrink();

        _notifier.Received(1).NotifySelectDrink();
    }

    [Test]
    public void Resets_Drink_After_Making_Drink()
    {
        AfterMakingDrink();

        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received(1).Send(Arg.Any<Order>());
        _notifier.Received(1).NotifySelectDrink();
    }

    [Test]
    public void Resets_Sugar_After_Making_Drink()
    {
        var ordersSent = CaptureSentOrders();
        AfterMakingDrinkWithSugar();

        _coffeeMachine.SelectCoffee();
        _coffeeMachine.MakeDrink();

        Assert.That(ordersSent.Last(), Is.EqualTo(Coffee().Build()));
    }

    [Test]
    public void Make_Tea_With_Not_Enough_Money()
    {
        var amount = 0.2m;
        _coffeeMachine = PaidCoffeeMachine();

        _coffeeMachine.SelectTea();
        _coffeeMachine.AddMoney(amount);
        _coffeeMachine.MakeDrink();

        _notifier.Received(1).NotifyMissingPrice(TeaPrice - amount);
    }

    [Test]
    public void Make_Coffee_With_Not_Enough_Money()
    {
        var amount = 0.3m;
        _coffeeMachine = PaidCoffeeMachine();

        _coffeeMachine.SelectCoffee();
        _coffeeMachine.AddMoney(amount);
        _coffeeMachine.MakeDrink();

        _notifier.Received(1).NotifyMissingPrice(CoffeePrice - amount);
    }

    [Test]
    public void Make_Chocolate_With_Not_Enough_Money()
    {
        var amount = 0.1m;
        _coffeeMachine = PaidCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddMoney(amount);
        _coffeeMachine.MakeDrink();

        _notifier.Received(1).NotifyMissingPrice(ChocolatePrice - amount);
    }

    [Test]
    public void Make_Tea_With_Enough_Money()
    {
        _coffeeMachine = PaidCoffeeMachine();

        _coffeeMachine.SelectTea();
        _coffeeMachine.AddMoney(0.2m);
        _coffeeMachine.AddMoney(0.2m);
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Tea().Build());
    }

    [Test]
    public void Reset_Money_After_Making_Drink()
    {
        AfterPayingAndMakingDrink();

        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();

        _notifier.Received(1).NotifyMissingPrice(TeaPrice - 0m);
    }

    private void AfterPayingAndMakingDrink()
    {
        _coffeeMachine = PaidCoffeeMachine();
        _coffeeMachine.SelectCoffee();
        _coffeeMachine.AddMoney(CoffeePrice);
        _coffeeMachine.MakeDrink();
    }

    private CoffeeMachine FreeCoffeeMachine()
    {
        var prices = new Dictionary<DrinkType, decimal>()
        {
            { DrinkType.Chocolate, 0 },
            { DrinkType.Coffee, 0 },
            { DrinkType.Tea, 0 }
        };
        return new CoffeeMachine(_drinkMakerDriver, _notifier, new LocalPricesCatalog(prices));
    }
    private CoffeeMachine PaidCoffeeMachine()
    {
        var prices = new Dictionary<DrinkType, decimal>()
        {
            { DrinkType.Chocolate, ChocolatePrice },
            { DrinkType.Coffee, CoffeePrice },
            { DrinkType.Tea, TeaPrice }
        };
        return new CoffeeMachine(_drinkMakerDriver, _notifier, new LocalPricesCatalog(prices));
    }

    private List<Order> CaptureSentOrders()
    {
        var ordersSent = new List<Order>();
        _drinkMakerDriver.Send(Arg.Do<Order>(order => ordersSent.Add(order)));
        return ordersSent;
    }

    private void AfterMakingDrink()
    {
        _coffeeMachine = FreeCoffeeMachine();
        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();
    }

    private void AfterMakingDrinkWithSugar()
    {
        _coffeeMachine = FreeCoffeeMachine();
        _coffeeMachine.SelectTea();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();
    }
}