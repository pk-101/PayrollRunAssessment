using Moq;
using Payroll.Application.DTOs;
using Payroll.Application.Interfaces;
using Payroll.Infrastructure.Services;

namespace Payroll.Tests.Services;

public class PayrollServiceTests
{
    [Fact]
    public async Task RunPayrollAsync_ShouldThrowException_WhenMonthIsLessThanOne()
    {
        // Arrange
        var repositoryMock =
            new Mock<IPayrollRepository>();

        var service =
            new PayrollService(repositoryMock.Object);

        var request =
            new RunPayrollRequestDto
            {
                Month = 0,
                Year = 2026
            };

        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<ArgumentException>(
                () => service.RunPayrollAsync(request));

        Assert.Equal(
            "Month must be between 1 and 12.",
            exception.Message);
    }

    [Fact]
    public async Task RunPayrollAsync_ShouldThrowException_WhenMonthIsGreaterThanTwelve()
    {
        var repositoryMock =
            new Mock<IPayrollRepository>();

        var service =
            new PayrollService(repositoryMock.Object);

        var request =
            new RunPayrollRequestDto
            {
                Month = 13,
                Year = 2026
            };

        var exception =
            await Assert.ThrowsAsync<ArgumentException>(
                () => service.RunPayrollAsync(request));

        Assert.Equal(
            "Month must be between 1 and 12.",
            exception.Message);
    }

    [Fact]
    public async Task RunPayrollAsync_ShouldThrowException_WhenYearIsInvalid()
    {
        var repositoryMock =
            new Mock<IPayrollRepository>();

        var service =
            new PayrollService(repositoryMock.Object);

        var request =
            new RunPayrollRequestDto
            {
                Month = 6,
                Year = 1999
            };

        var exception =
            await Assert.ThrowsAsync<ArgumentException>(
                () => service.RunPayrollAsync(request));

        Assert.Equal(
            "Invalid year.",
            exception.Message);
    }

    [Fact]
    public async Task RunPayrollAsync_ShouldReturnPayrollRunId_WhenRequestIsValid()
    {
        // Arrange
        var repositoryMock =
            new Mock<IPayrollRepository>();

        repositoryMock
            .Setup(x => x.RunPayrollAsync(6, 2026))
            .ReturnsAsync(1);

        var service =
            new PayrollService(repositoryMock.Object);

        var request =
            new RunPayrollRequestDto
            {
                Month = 6,
                Year = 2026
            };

        // Act
        var result =
            await service.RunPayrollAsync(request);

        // Assert
        Assert.Equal(1, result);

        repositoryMock.Verify(
            x => x.RunPayrollAsync(6, 2026),
            Times.Once);
    }

    [Fact]
    public async Task GetPayrollAsync_ShouldReturnPayrollData()
    {
        // Arrange
        var repositoryMock =
            new Mock<IPayrollRepository>();

        var payrollData =
            new List<PayrollResponseDto>
            {
            new()
            {
                EmployeeId = 1,
                Name = "Ravi Sharma",
                NetPay = 25000
            }
            };

        repositoryMock
            .Setup(x => x.GetPayrollAsync(6, 2026))
            .ReturnsAsync(payrollData);

        var service =
            new PayrollService(repositoryMock.Object);

        // Act
        var result =
            await service.GetPayrollAsync(6, 2026);

        // Assert
        Assert.Single(result);

        repositoryMock.Verify(
            x => x.GetPayrollAsync(6, 2026),
            Times.Once);
    }

    [Fact]
    public async Task GetPayslipAsync_ShouldReturnPayslip()
    {
        // Arrange
        var repositoryMock =
            new Mock<IPayrollRepository>();

        var payslip =
            new PayslipDto
            {
                PayrollRunId = 1,
                EmployeeId = 1,
                EmployeeName = "Ravi Sharma",
                NetPay = 25000
            };

        repositoryMock
            .Setup(x =>
                x.GetPayslipAsync(1, 1))
            .ReturnsAsync(payslip);

        var service =
            new PayrollService(repositoryMock.Object);

        // Act
        var result =
            await service.GetPayslipAsync(1, 1);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(
            "Ravi Sharma",
            result!.EmployeeName);

        repositoryMock.Verify(
            x => x.GetPayslipAsync(1, 1),
            Times.Once);
    }
}