using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

public partial class StockBrokerClient
{
    public class OperationsSummaryFormatter
    {
        private readonly CultureInfo _currentCultureInfo;
        private readonly Clock _clock;
        private const string StandarFormatCode = "g";

        public OperationsSummaryFormatter(CultureInfo currentCultureInfo, Clock clock)
        {
            _currentCultureInfo = currentCultureInfo;
            _clock = clock;
        }

        public string FormatMessage(List<Order> orderList)
        {
            var dateTimeOrder = _clock.Get();
            var dateTimerOrderFormated = dateTimeOrder.ToString(StandarFormatCode, _currentCultureInfo);
            var totalSellPrice = 0.00m;
            var totalBuyPrice = 0.00m;

            foreach (var order in orderList)
            {
                if (order.isBuy)
                {
                    totalBuyPrice += order.GetTotalPrice();
                }
                else
                {
                    totalSellPrice += order.GetTotalPrice();
                }
            }
            
            return $"{dateTimerOrderFormated} Buy: \u20ac {totalBuyPrice.ToString(StandarFormatCode, _currentCultureInfo)}, Sell: \u20ac {totalSellPrice.ToString(StandarFormatCode, _currentCultureInfo)}";
        }
    }
}