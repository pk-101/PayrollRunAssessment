using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;

namespace Payroll.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees =
            await _employeeRepository.GetEmployeesAsync();

        return Ok(employees);
    }
}