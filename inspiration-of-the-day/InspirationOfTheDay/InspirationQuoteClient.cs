using System;
using System.Collections.Generic;
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
        var quoteSelected = GetQuote(word);
        var employeeSelected = GetEmployee();

        _notifier.Notify(quoteSelected, employeeSelected);
    }

    private Employee GetEmployee()
    {
        var employees = _employeeRepository.GetAll();
        var indexEmployee = _randomItemGenerator.Get(employees.Count - 1);
        return employees[indexEmployee];
    }

    private string GetQuote(string word)
    {
        var quotes = _quoteService.Get(word);
        var indexQuote = _randomItemGenerator.Get(quotes.Count - 1);
        return quotes[indexQuote];
    }
}