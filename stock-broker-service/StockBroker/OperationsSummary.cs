using System.Collections.Generic;
using System.Linq;

namespace StockBroker;

public partial class StockBrokerClient
{
    public class OperationsSummary
    {
        private readonly List<Order> _orderList;

        public OperationsSummary(List<Order> orderList)
        {
            _orderList = orderList;
        }

        public decimal GetBuy()
        {
            return _orderList
                .Where(order => order.isBuy)
                .Sum(order => order.GetTotalPrice());
        }

        public decimal GetSell()
        {
            return _orderList
                .Where(order => !order.isBuy)
                .Sum(order => order.GetTotalPrice());
        }
    }
}