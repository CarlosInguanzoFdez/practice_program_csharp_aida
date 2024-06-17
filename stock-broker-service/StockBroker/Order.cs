namespace StockBroker;

public record Order(string ticker, int quantity, decimal price, string orderType);
