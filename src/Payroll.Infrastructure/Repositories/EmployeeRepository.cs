using Dapper;
using Payroll.Application.DTOs;
using Payroll.Application.Interfaces;
using Payroll.Infrastructure.Data;

namespace Payroll.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public EmployeeRepository(
        DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
    {
        const string sql = @"
            SELECT
                EmployeeId,
                EmployeeCode,
                Name,
                BasicSalary
            FROM Employees
            WHERE IsActive = 1
            ORDER BY Name";

        using var connection =
            _connectionFactory.CreateConnection();

        return await connection.QueryAsync<EmployeeDto>(
            sql);
    }
}