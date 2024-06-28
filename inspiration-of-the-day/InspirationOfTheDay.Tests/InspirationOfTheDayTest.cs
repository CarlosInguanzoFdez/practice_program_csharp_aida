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

    [SetUp]
    public void Setup()
    {
        _notifier = Substitute.For<Notifier>();
        _employeeRepository = Substitute.For<EmployeeRepository>();
        _quoteService = Substitute.For<QuoteService>();
        _inspirationQuoteClient = new InspirationQuoteClient(_employeeRepository, _notifier, _quoteService);
    }

    [Test]
    public void get_random_quote_and_notify_employee()
    {
        var inputWord = "simple";
        var mobile = "656874112";
        var employeeSelected = new Employee(mobile);
        var employees = new List<Employee> { employeeSelected };
        _quoteService.Get(inputWord).Returns(new List<string>() { "Es simple: solo haz que pase" });
        _employeeRepository.GetAll().Returns(employees);

        _inspirationQuoteClient.InspireSomenone(inputWord);
        
        _notifier.Received(1).Notify("Es simple: solo haz que pase", employeeSelected);
    }

    /*
     Casos opss:
        - No se obtiene ning�n empleado del repositorio
        - No se obtiene ninguna cita del servicio web
        - Un fallo en el envio del whatssap
     */
}