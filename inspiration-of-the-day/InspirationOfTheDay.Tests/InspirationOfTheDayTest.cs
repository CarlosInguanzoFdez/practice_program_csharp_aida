using NSubstitute;
using NUnit.Framework;

namespace InspirationOfTheDay.Tests;

public class InspirationOfTheDayTest
{
    private Notifier _notifier;
    private EmployeeRepository _employeeRepository;
    private QuoteService _quoteService;
    private InspirationQuoteClient _inspirationQuoteClient;


    [Test]
    public void Fix_Me_And_Rename_Me()
    {
        _notifier = Substitute.For<Notifier>();
        _employeeRepository = Substitute.For<EmployeeRepository>();
        _quoteService = Substitute.For<QuoteService>();
        _inspirationQuoteClient = Substitute.For<InspirationQuoteClient>();

        _inspirationQuoteClient.InspireSomenone("avanzando");

        Assert.That(false, Is.True);
    }
}