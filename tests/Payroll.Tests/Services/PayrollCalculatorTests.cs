using Payroll.Application.Services;

namespace Payroll.Tests.Services;

public class PayrollCalculatorTests
{
    [Fact]
    public void CalculateNetPay_ShouldReturnExpectedNetPay()
    {
        // Arrange
        decimal basicSalary = 50000m;
        int workingDays = 30;
        int daysPresent = 22;

        // Act
        var result = PayrollCalculator.CalculateNetPay(
            basicSalary,
            workingDays,
            daysPresent);

        // Assert
        Assert.Equal(30466.67m, result);
    }

    [Fact]
    public void CalculateGrossPay_ShouldReturnRoundedGrossPay()
    {
        // Arrange
        decimal basicSalary = 50000m;
        int workingDays = 30;
        int daysPresent = 22;

        // Act
        var result = PayrollCalculator.CalculateGrossPay(
            basicSalary,
            workingDays,
            daysPresent);

        // Assert
        Assert.Equal(36666.67m, result);
    }

    [Fact]
    public void CalculatePFDeduction_ShouldReturnTwelvePercentOfBasicSalary()
    {
        // Arrange
        decimal basicSalary = 50000m;

        // Act
        var result = PayrollCalculator.CalculatePFDeduction(basicSalary);

        // Assert
        Assert.Equal(6000.00m, result);
    }

    [Fact]
    public void CalculateGrossPay_ShouldThrow_WhenWorkingDaysIsZero()
    {
        // Arrange
        decimal basicSalary = 50000m;
        int workingDays = 0;
        int daysPresent = 22;

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            PayrollCalculator.CalculateGrossPay(
                basicSalary,
                workingDays,
                daysPresent));
    }
}
