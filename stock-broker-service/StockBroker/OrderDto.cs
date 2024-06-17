namespace StockBroker;

public record StockOrderDto(string ticker, int quantity, decimal price);