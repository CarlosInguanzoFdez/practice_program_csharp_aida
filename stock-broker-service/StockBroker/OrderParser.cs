using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

public partial class StockBrokerClient
{
    public class OrderParser
    {
        private readonly CultureInfo _currentCultureInfo;

        public OrderParser(CultureInfo currentCultureInfo)
        {
            _currentCultureInfo = currentCultureInfo;
        }

        public Order Parse(string orderSequence)
        {
            if (string.IsNullOrEmpty(orderSequence)) {
                return new Order("", 0, 0.00m, true);
            }

            return ParseSequence(orderSequence);
        }

        public List<Order> MultipleParse(string sequence)
        {
            var orderList = new List<Order>();
            if (string.IsNullOrEmpty(sequence)) {
                return orderList;
            }
            var ordersSequence = sequence.Split(",");
            foreach (var orderSequence in ordersSequence)
            {
                orderList.Add(ParseSequence(orderSequence));
            }

            return orderList;
        }

        private Order ParseSequence(string orderSequence)
        {
            var paramsOrder = orderSequence.Split(" ");
            var price = decimal.Parse(paramsOrder[2], _currentCultureInfo);
            bool isBuy = paramsOrder[3] == "B"; 

            var order = new Order(paramsOrder[0], int.Parse(paramsOrder[1]), price, isBuy);
            return order;
        }
    }
}