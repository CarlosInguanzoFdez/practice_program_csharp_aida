using System.Globalization;
using Microsoft.VisualBasic;

namespace Hello;

public class ServiceHello
{
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

        if (date.Hour >= 6 && date.Hour < 12) {
            _notify.Notify("Buenos d�as");
            return;
        }

        if (date.Hour >= 12 && date.Hour < 20)
        {
            _notify.Notify("Buenas tardes");
            return;
        }

        _notify.Notify("Buenas noches");
    }
}