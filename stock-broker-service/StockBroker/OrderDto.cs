namespace StockBroker;

public record OrderDto(string ticker, int quantity, decimal price);