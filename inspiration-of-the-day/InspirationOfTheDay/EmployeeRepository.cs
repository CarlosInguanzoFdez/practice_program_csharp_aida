namespace InspirationOfTheDay;

public interface EmployeeRepository
{
    Employee Get();
}

public record Employee(string mobile);