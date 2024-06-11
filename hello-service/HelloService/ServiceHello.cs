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

        if (date.Hour >= 6 && date.Hour >= 12) {
            _notify.Notify("Buenas tardes");
        }
            
        _notify.Notify("Buenos días");
    }
}