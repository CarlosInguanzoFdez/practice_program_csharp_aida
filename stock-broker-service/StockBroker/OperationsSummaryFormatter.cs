using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

public partial class StockBrokerClient
{
    public class OperationsSummaryFormatter
    {
        private readonly CultureInfo _currentCultureInfo;
        private readonly Clock _clock;
        private const string DateFormatCode = "g";
        private const string DecimalFormatCode = "0.00";

        public OperationsSummaryFormatter(CultureInfo currentCultureInfo, Clock clock)
        {
            _currentCultureInfo = currentCultureInfo;
            _clock = clock;
        }

        public string FormatMessage(List<Order> orderList)
        {
            var dateTimeOrder = _clock.Get();
            var dateTimerOrderFormated = dateTimeOrder.ToString(DateFormatCode, _currentCultureInfo);
            var summary = new OperationsSummary(orderList);

            return $"{dateTimerOrderFormated} Buy: \u20ac {FormatDecimal(summary.GetBuy())}, Sell: \u20ac {FormatDecimal(summary.GetSell())}";
        }

        public string FormatDecimal(decimal value)
        {
            return value.ToString(DecimalFormatCode, _currentCultureInfo);
        }
    }
}