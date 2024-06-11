using System;
using System.Globalization;
using Microsoft.VisualBasic;

namespace Hello;

public class ServiceHello
{
    private const string GoodMorning = "Buenos días";
    private const string GoodAfternoon = "Buenas tardes";
    private const string GoodNight = "Buenas noches";
    private readonly Notifier _notify;
    private readonly Clock _clock;

    public ServiceHello(Notifier notify, Clock clock)
    {
        _notify = notify;
        _clock = clock;
    }

    public void Hello()
    {
        var date = _clock.Get();

        if (IsMorning(date)) {
            _notify.Notify(GoodMorning);
            return;
        }

        if (IsAfternoon(date))
        {
            _notify.Notify(GoodAfternoon);
            return;
        }

        _notify.Notify(GoodNight);
    }

    private bool IsAfternoon(DateTime date)
    {
        return date.Hour >= 12 && date.Hour < 20;
    }

    private bool IsMorning(DateTime date)
    {
        return date.Hour >= 6 && date.Hour < 12;
    }
}