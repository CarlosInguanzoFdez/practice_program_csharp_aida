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
        _employeeRepository.Get().Returns(new Employee("656874112"));

        _inspirationQuoteClient.InspireSomenone("avanzando");

        _quoteService.Received(1).Get("avanzando");
        _notifier.Received(1).Notify("Si no puedes volar, corre; si no puedes correr, camina; si no puedes caminar, gatea, pero sigue avanzando hacia tu meta");
    }

    /*
     Casos opss:
        - No se obtiene ningún empleado del repositorio
        - No se obtiene ninguna cita del servicio web
        - Un fallo en el envio del whatssap
     */
}