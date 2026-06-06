USE PayrollDB;
GO

----------------------------------------------------
-- Departments
----------------------------------------------------
CREATE TABLE Departments
(
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL
);
GO

----------------------------------------------------
-- Employees
----------------------------------------------------
CREATE TABLE Employees
(
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,

    EmployeeCode NVARCHAR(20) NOT NULL,
    Name NVARCHAR(200) NOT NULL,

    DepartmentId INT NOT NULL,

    BasicSalary DECIMAL(18,2) NOT NULL,

    IsActive BIT NOT NULL DEFAULT(1),

    CreatedDate DATETIME2 NOT NULL DEFAULT(GETDATE()),

    CONSTRAINT UQ_Employees_EmployeeCode
        UNIQUE(EmployeeCode),

    CONSTRAINT FK_Employees_Departments
        FOREIGN KEY (DepartmentId)
        REFERENCES Departments(DepartmentId)
);
GO

----------------------------------------------------
-- Attendance
----------------------------------------------------
CREATE TABLE Attendance
(
    AttendanceId INT IDENTITY(1,1) PRIMARY KEY,

    EmployeeId INT NOT NULL,

    [Month] INT NOT NULL,
    [Year] INT NOT NULL,

    WorkingDays INT NOT NULL,
    DaysPresent INT NOT NULL,

    CONSTRAINT FK_Attendance_Employees
        FOREIGN KEY(EmployeeId)
        REFERENCES Employees(EmployeeId),

    CONSTRAINT UQ_Attendance
        UNIQUE(EmployeeId, [Month], [Year])
);
GO

----------------------------------------------------
-- PayrollRuns
----------------------------------------------------
CREATE TABLE PayrollRuns
(
    PayrollRunId INT IDENTITY(1,1) PRIMARY KEY,

    [Month] INT NOT NULL,
    [Year] INT NOT NULL,

    RunDate DATETIME2 NOT NULL DEFAULT(GETDATE()),

    CONSTRAINT UQ_PayrollRuns
        UNIQUE([Month], [Year])
);
GO

----------------------------------------------------
-- PayrollDetails
----------------------------------------------------
CREATE TABLE PayrollDetails
(
    PayrollDetailId INT IDENTITY(1,1) PRIMARY KEY,

    PayrollRunId INT NOT NULL,
    EmployeeId INT NOT NULL,

    BasicSalary DECIMAL(18,2) NOT NULL,

    WorkingDays INT NOT NULL,
    DaysPresent INT NOT NULL,

    GrossPay DECIMAL(18,2) NOT NULL,
    PFDeduction DECIMAL(18,2) NOT NULL,
    ProfessionalTax DECIMAL(18,2) NOT NULL,
    NetPay DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_PayrollDetails_PayrollRuns
        FOREIGN KEY(PayrollRunId)
        REFERENCES PayrollRuns(PayrollRunId),

    CONSTRAINT FK_PayrollDetails_Employees
        FOREIGN KEY(EmployeeId)
        REFERENCES Employees(EmployeeId)
);
GO

----------------------------------------------------
-- Indexes
----------------------------------------------------

CREATE INDEX IX_Employees_DepartmentId
ON Employees(DepartmentId);
GO

CREATE INDEX IX_Attendance_EmployeeId
ON Attendance(EmployeeId);
GO

CREATE INDEX IX_PayrollDetails_PayrollRunId
ON PayrollDetails(PayrollRunId);
GO

CREATE INDEX IX_PayrollDetails_EmployeeId
ON PayrollDetails(EmployeeId);
GO