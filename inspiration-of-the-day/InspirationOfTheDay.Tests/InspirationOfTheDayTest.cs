using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace InspirationOfTheDay.Tests;

public class InspirationOfTheDayTest
{
    private Notifier _notifier;
    private EmployeeRepository _employeeRepository;
    private QuoteService _quoteService;
    private InspirationQuoteClient _inspirationQuoteClient;
    private RandomItemGenerator _randomItemGenerator;

    [SetUp]
    public void Setup()
    {
        _notifier = Substitute.For<Notifier>();
        _employeeRepository = Substitute.For<EmployeeRepository>();
        _quoteService = Substitute.For<QuoteService>();
        _randomItemGenerator = Substitute.For<RandomItemGenerator>();
        _inspirationQuoteClient = new InspirationQuoteClient(_employeeRepository, _notifier, _quoteService, _randomItemGenerator);
    }

    [Test]
    public void get_the_unique_quote_and_notify_unique_employee()
    {
        var inputWord = "simple";
        var mobile = "656874112";
        var employeeSelected = new Employee(mobile);
        var employees = new List<Employee> { employeeSelected };
        _quoteService.Get(inputWord).Returns(new List<string>() { "Es simple: solo haz que pase" });
        _randomItemGenerator.Get(Arg.Any<int>()).Returns(0);
        _employeeRepository.GetAll().Returns(employees);

        _inspirationQuoteClient.InspireSomenone(inputWord);
        
        _notifier.Received(1).Notify("Es simple: solo haz que pase", employeeSelected);
    }

    [Test]
    public void get_random_quote_and_notify_the_unique_employee()
    {
        var inputWord = "simple";
        var mobile = "656874112";
        var employeeSelected = new Employee(mobile);
        var employees = new List<Employee> { employeeSelected };
        _quoteService.Get(inputWord).Returns(new List<string>() { "Es simple: solo haz que pase", "Todo es mas simple de lo que parece" });
        _randomItemGenerator.Get(Arg.Any<int>()).Returns(0);
        _employeeRepository.GetAll().Returns(employees);

        _inspirationQuoteClient.InspireSomenone(inputWord);

        _notifier.Received(1).Notify("Es simple: solo haz que pase", employeeSelected);
    }

    /*
     Casos opss:
        - No se obtiene ningún empleado del repositorio
        - No se obtiene ninguna cita del servicio web
        - Un fallo en el envio del whatssap
     */
}