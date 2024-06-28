using System;
using System.Drawing;
using System.Linq;

namespace InspirationOfTheDay;

public class InspirationQuoteClient
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly Notifier _notifier;
    private readonly QuoteService _quoteService;
    private readonly RandomItemGenerator _randomItemGenerator;

    public InspirationQuoteClient(EmployeeRepository employeeRepository, Notifier notifier, QuoteService quoteService,
        RandomItemGenerator randomItemGenerator)
    {
        _employeeRepository = employeeRepository;
        _notifier = notifier;
        _quoteService = quoteService;
        _randomItemGenerator = randomItemGenerator;
    }

    public void InspireSomenone(string word)
    {
        var quotes = _quoteService.Get(word);
        var indexQuote = _randomItemGenerator.Get(quotes.Count - 1);
        new Random().Next(quotes.Count);
        var employees = _employeeRepository.GetAll();
        var indexEmployee = _randomItemGenerator.Get(employees.Count - 1);

        _notifier.Notify(quotes[indexQuote], employees[indexEmployee]);
    }
}