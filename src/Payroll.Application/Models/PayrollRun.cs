namespace Payroll.Application.Models;

public class PayrollRun
{
    public int PayrollRunId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public DateTime RunDate { get; set; }
}