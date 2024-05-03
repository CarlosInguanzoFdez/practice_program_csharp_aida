namespace CoffeeMachineApp.core;

public interface PricesCatalog
{
    decimal GetPrice(DrinkType drinkType);
}