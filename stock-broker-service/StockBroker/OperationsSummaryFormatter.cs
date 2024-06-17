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

        public string FormatMessage(Order order)
        {
            var dateTimeOrder = _clock.Get();
            var dateTimerOrderFormated = dateTimeOrder.ToString(StandarFormatCode, _currentCultureInfo);
            if (order.isBuy)
            {
                return $"{dateTimerOrderFormated} Buy: \u20ac {order.GetTotalPrice().ToString(StandarFormatCode, _currentCultureInfo)}, Sell: \u20ac 0.00";
            }
            return $"{dateTimerOrderFormated} Buy: \u20ac 0.00, Sell: \u20ac {order.GetTotalPrice().ToString(StandarFormatCode, _currentCultureInfo)}";
        }
    }
}