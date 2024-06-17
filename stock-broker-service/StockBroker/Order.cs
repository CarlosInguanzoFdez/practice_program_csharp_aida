namespace StockBroker;

public record Order(string ticker, int quantity, decimal price, bool isBuy)
{
    public decimal GetTotalPrice()
    {
        var totalBuy = price * quantity;
        return totalBuy;
    }
}
