using System.Collections.Generic;

namespace InspirationOfTheDay;

public interface EmployeeRepository
{
    List<Employee> GetAll();
}