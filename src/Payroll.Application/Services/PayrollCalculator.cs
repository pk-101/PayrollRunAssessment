namespace Payroll.Application.Services;

public static class PayrollCalculator
{
    public const decimal ProfessionalTax = 200.00m;
    private const decimal PfRate = 0.12m;

    public static decimal CalculateGrossPay(
        decimal basicSalary,
        int workingDays,
        int daysPresent)
    {
        if (workingDays <= 0)
        {
            throw new ArgumentException(
                "Working days must be greater than zero.",
                nameof(workingDays));
        }

        return Math.Round(
            (basicSalary / workingDays) * daysPresent,
            2,
            MidpointRounding.AwayFromZero);
    }

    public static decimal CalculatePFDeduction(decimal basicSalary)
    {
        return Math.Round(
            basicSalary * PfRate,
            2,
            MidpointRounding.AwayFromZero);
    }

    public static decimal CalculateNetPay(
        decimal basicSalary,
        int workingDays,
        int daysPresent)
    {
        var grossPay = CalculateGrossPay(
            basicSalary,
            workingDays,
            daysPresent);

        var pfDeduction = CalculatePFDeduction(basicSalary);

        return Math.Round(
            grossPay - pfDeduction - ProfessionalTax,
            2,
            MidpointRounding.AwayFromZero);
    }
}
