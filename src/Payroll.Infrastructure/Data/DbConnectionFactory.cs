using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Payroll.Infrastructure.Configurations;
using System.Data;

namespace Payroll.Infrastructure.Data;

public class DbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(
        IOptions<DatabaseSettings> options)
    {
        _connectionString =
            options.Value.ConnectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(
            _connectionString);
    }
}