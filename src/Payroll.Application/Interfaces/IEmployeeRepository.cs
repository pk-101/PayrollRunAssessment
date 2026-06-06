using Payroll.Application.DTOs;

namespace Payroll.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
}