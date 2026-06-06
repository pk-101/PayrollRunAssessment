USE PayrollDB;
GO

CREATE OR ALTER PROCEDURE usp_RunPayroll
(
    @Month INT,
    @Year INT
)
AS
BEGIN

    SET NOCOUNT ON;

    ------------------------------------------------
    -- Prevent Duplicate Payroll Runs
    ------------------------------------------------
    IF EXISTS
    (
        SELECT 1
        FROM PayrollRuns
        WHERE [Month] = @Month
        AND [Year] = @Year
    )
    BEGIN
        THROW 50001,
              'Payroll already exists for the selected month and year.',
              1;
    END;

    BEGIN TRANSACTION;

    BEGIN TRY

        ------------------------------------------------
        -- Create Payroll Run
        ------------------------------------------------
        INSERT INTO PayrollRuns
        (
            [Month],
            [Year]
        )
        VALUES
        (
            @Month,
            @Year
        );

        DECLARE @PayrollRunId INT =
            SCOPE_IDENTITY();

        ------------------------------------------------
        -- Create Payroll Details
        ------------------------------------------------
        INSERT INTO PayrollDetails
        (
            PayrollRunId,
            EmployeeId,
            BasicSalary,
            WorkingDays,
            DaysPresent,
            GrossPay,
            PFDeduction,
            ProfessionalTax,
            NetPay
        )
        SELECT
            @PayrollRunId,

            E.EmployeeId,

            E.BasicSalary,

            A.WorkingDays,

            A.DaysPresent,

            ROUND(
                (E.BasicSalary / A.WorkingDays)
                * A.DaysPresent,
                2
            ) AS GrossPay,

            ROUND(
                E.BasicSalary * 0.12,
                2
            ) AS PFDeduction,

            200.00 AS ProfessionalTax,

            ROUND(
                (
                    (E.BasicSalary / A.WorkingDays)
                    * A.DaysPresent
                )
                -
                (E.BasicSalary * 0.12)
                -
                200,
                2
            ) AS NetPay

        FROM Employees E
        INNER JOIN Attendance A
            ON E.EmployeeId = A.EmployeeId

        WHERE
            A.[Month] = @Month
            AND A.[Year] = @Year
            AND E.IsActive = 1;

        COMMIT TRANSACTION;

        ------------------------------------------------
        -- Return Summary
        ------------------------------------------------
        SELECT
            @PayrollRunId AS PayrollRunId;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH

END;
GO