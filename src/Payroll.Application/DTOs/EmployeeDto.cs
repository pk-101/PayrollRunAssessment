namespace Payroll.Application.DTOs;

public class EmployeeDto
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal BasicSalary { get; set; }
}