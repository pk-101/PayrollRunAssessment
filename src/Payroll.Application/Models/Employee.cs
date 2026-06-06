namespace Payroll.Application.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public decimal BasicSalary { get; set; }

    public bool IsActive { get; set; }
}