namespace StockBroker;

public record Order(string ticker, int quantity, decimal price, bool isBuy)
{
    public decimal GetTotalPrice()
    {
        var totalBuy = this.price * this.quantity;
        return totalBuy;
    }
}
