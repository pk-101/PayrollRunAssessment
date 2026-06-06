namespace Payroll.Application.Models;

public class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public int WorkingDays { get; set; }

    public int DaysPresent { get; set; }
}