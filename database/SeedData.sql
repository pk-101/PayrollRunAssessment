USE PayrollDB;
GO

----------------------------------------------------
-- Departments
----------------------------------------------------

INSERT INTO Departments (DepartmentName)
VALUES
('Human Resources'),
('Information Technology');
GO

----------------------------------------------------
-- Employees
----------------------------------------------------

INSERT INTO Employees
(
    EmployeeCode,
    Name,
    DepartmentId,
    BasicSalary
)
VALUES
('EMP001', 'Ravi Sharma',      1, 30000),
('EMP002', 'Priya Verma',      1, 35000),
('EMP003', 'Amit Kumar',       2, 45000),
('EMP004', 'Neha Singh',       2, 50000),
('EMP005', 'Vikram Patel',     2, 40000);
GO

----------------------------------------------------
-- Attendance
----------------------------------------------------
-- June 2026 Example
----------------------------------------------------

INSERT INTO Attendance
(
    EmployeeId,
    [Month],
    [Year],
    WorkingDays,
    DaysPresent
)
VALUES
(1, 6, 2026, 26, 24),
(2, 6, 2026, 26, 26),
(3, 6, 2026, 26, 23),
(4, 6, 2026, 26, 25),
(5, 6, 2026, 26, 22);
GO