using System;
using System.Drawing;
using System.Linq;

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
        var quotes = _quoteService.Get(word);
        var randomQuote = new Random().Next(quotes.Count);

        var employees = _employeeRepository.GetAll();
        var employeeSelected = employees.First();

        _notifier.Notify(quotes[randomQuote], employeeSelected);
    }
}