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
        _notify.Notify("Buenos días");
    }
}