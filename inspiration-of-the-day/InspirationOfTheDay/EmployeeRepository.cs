using System.Collections.Generic;

namespace InspirationOfTheDay;

public interface EmployeeRepository
{
    Employee Get();
    List<Employee> GetAll();
}