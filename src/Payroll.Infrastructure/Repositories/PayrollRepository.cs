using Dapper;
using Payroll.Application.DTOs;
using Payroll.Application.Interfaces;
using Payroll.Infrastructure.Data;
using System.Data;

namespace Payroll.Infrastructure.Repositories;

public class PayrollRepository : IPayrollRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public PayrollRepository(
        DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> RunPayrollAsync(
        int month,
        int year)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "usp_RunPayroll",
            new
            {
                Month = month,
                Year = year
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PayrollResponseDto>>
        GetPayrollAsync(
            int month,
            int year)
    {
        const string sql = @"
SELECT
    pd.PayrollRunId,
    pd.EmployeeId,
    e.Name,
    pd.BasicSalary,
    pd.WorkingDays,
    pd.DaysPresent,
    pd.GrossPay,
    pd.PFDeduction,
    pd.ProfessionalTax,
    pd.NetPay
FROM PayrollDetails pd
INNER JOIN PayrollRuns pr
    ON pd.PayrollRunId = pr.PayrollRunId
INNER JOIN Employees e
    ON pd.EmployeeId = e.EmployeeId
WHERE pr.Month = @Month
AND pr.Year = @Year
ORDER BY e.Name";

        using var connection =
            _connectionFactory.CreateConnection();

        return await connection.QueryAsync<
            PayrollResponseDto>(
            sql,
            new
            {
                Month = month,
                Year = year
            });
    }

    public async Task<PayslipDto?> GetPayslipAsync(
        int payrollRunId,
        int employeeId)
    {
        const string sql = @"
SELECT
    pd.PayrollRunId,
    pd.EmployeeId,
    e.Name AS EmployeeName,
    pd.BasicSalary,
    pd.WorkingDays,
    pd.DaysPresent,
    pd.GrossPay,
    pd.PFDeduction,
    pd.ProfessionalTax,
    pd.NetPay
FROM PayrollDetails pd
INNER JOIN Employees e
    ON pd.EmployeeId = e.EmployeeId
WHERE pd.PayrollRunId = @PayrollRunId
AND pd.EmployeeId = @EmployeeId";

        using var connection =
            _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<
            PayslipDto>(
            sql,
            new
            {
                PayrollRunId = payrollRunId,
                EmployeeId = employeeId
            });
    }
}