namespace Payroll.Application.Models;

public class PayrollDetail
{
    public int PayrollDetailId { get; set; }

    public int PayrollRunId { get; set; }

    public int EmployeeId { get; set; }

    public decimal BasicSalary { get; set; }

    public int WorkingDays { get; set; }

    public int DaysPresent { get; set; }

    public decimal GrossPay { get; set; }

    public decimal PFDeduction { get; set; }

    public decimal ProfessionalTax { get; set; }

    public decimal NetPay { get; set; }
}