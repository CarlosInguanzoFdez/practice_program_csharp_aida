using System.Drawing;

namespace InspirationOfTheDay;

public class InspirationQuoteClient
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly Notifier _notifier;
    private readonly QuoteService _quoteService;

    public InspirationQuoteClient(EmployeeRepository employeeRepository, Notifier notifier, QuoteService quoteService)
    {
        _employeeRepository = employeeRepository;
        _notifier = notifier;
        _quoteService = quoteService;
    }

    public void InspireSomenone(string word)
    {
        _quoteService.Get(word);
        _notifier.Notify("Si no puedes volar, corre; si no puedes correr, camina; si no puedes caminar, gatea, pero sigue avanzando hacia tu meta");
    }
}