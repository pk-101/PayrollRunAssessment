using Payroll.Application.DTOs;

namespace Payroll.Application.Interfaces;

public interface IPayrollService
{
    Task<int> RunPayrollAsync(
        RunPayrollRequestDto request);

    Task<IEnumerable<PayrollResponseDto>> GetPayrollAsync(
        int month,
        int year);

    Task<PayslipDto?> GetPayslipAsync(
        int payrollRunId,
        int employeeId);
}