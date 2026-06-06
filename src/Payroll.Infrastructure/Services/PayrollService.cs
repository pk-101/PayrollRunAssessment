using Payroll.Application.DTOs;
using Payroll.Application.Interfaces;

namespace Payroll.Infrastructure.Services;

public class PayrollService : IPayrollService
{
    private readonly IPayrollRepository _payrollRepository;

    public PayrollService(
        IPayrollRepository payrollRepository)
    {
        _payrollRepository = payrollRepository;
    }

    public async Task<int> RunPayrollAsync(
        RunPayrollRequestDto request)
    {
        if (request.Month < 1 || request.Month > 12)
        {
            throw new ArgumentException(
                "Month must be between 1 and 12.");
        }

        if (request.Year < 2000)
        {
            throw new ArgumentException(
                "Invalid year.");
        }

        return await _payrollRepository.RunPayrollAsync(
            request.Month,
            request.Year);
    }

    public async Task<IEnumerable<PayrollResponseDto>>
        GetPayrollAsync(
            int month,
            int year)
    {
        return await _payrollRepository.GetPayrollAsync(
            month,
            year);
    }

    public async Task<PayslipDto?>
        GetPayslipAsync(
            int payrollRunId,
            int employeeId)
    {
        return await _payrollRepository.GetPayslipAsync(
            payrollRunId,
            employeeId);
    }
}