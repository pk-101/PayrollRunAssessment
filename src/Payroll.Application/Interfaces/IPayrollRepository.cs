using Payroll.Application.DTOs;

namespace Payroll.Application.Interfaces;

public interface IPayrollRepository
{
    Task<int> RunPayrollAsync(int month, int year);

    Task<IEnumerable<PayrollResponseDto>> GetPayrollAsync(
        int month,
        int year);

    Task<PayslipDto?> GetPayslipAsync(
        int payrollRunId,
        int employeeId);
}