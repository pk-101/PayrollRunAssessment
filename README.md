# Payroll Management System

## Overview

This project is a Payroll Management System developed as part of a take-home technical assessment.

The application calculates monthly payroll based on employee attendance records and provides payroll summaries and employee payslips.

---

## Technology Stack

### Backend

* ASP.NET Core Web API
* C#
* Dapper
* SQL Server

### Frontend

* HTML
* CSS
* JavaScript

### Testing

* xUnit
* Moq

---

## Architecture

The solution follows a layered architecture:

```text
Payroll.API
    Controllers

Payroll.Application
    DTOs
    Interfaces
    Models

Payroll.Infrastructure
    Repositories
    Services
    Data
    Configurations

Payroll.Tests
```

### Layers

* API Layer: Exposes REST endpoints.
* Service Layer: Contains business validation and payroll orchestration.
* Repository Layer: Handles database access using Dapper.
* Database Layer: SQL Server tables and stored procedures.

---

## Database Setup

Execute the SQL scripts in the following order:

1. CreateTables.sql
2. SeedData.sql
3. StoredProcedures.sql

All scripts are located in:

```text
/database
```

---

## Configuration

Update the connection string in:

```text
src/Payroll.API/appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=Acer\\SQLEXPRESS;Database=PayrollDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## Running the Application

Restore packages:

```bash
dotnet restore
```

Build:

```bash
dotnet build
```

Run:

```bash
dotnet run --project src/Payroll.API
```

Open:

```text
https://localhost:5174
```

Swagger:

```text
https://localhost:5174/swagger
```

---

## API Endpoints

### Run Payroll

POST

```text
/api/payroll/run
```

Request:

```json
{
  "month": 6,
  "year": 2026
}
```

---

### Payroll Summary

GET

```text
/api/payroll/{month}/{year}
```

Example:

```text
/api/payroll/6/2026
```

---

### Employee Payslip

GET

```text
/api/payroll/{runId}/slip/{employeeId}
```

Example:

```text
/api/payroll/1/slip/1
```

---

## Business Rules

* Payroll can only be generated once per month and year.
* Month must be between 1 and 12.
* Year must be greater than or equal to 2000.
* Employees are paid proportionally based on attendance.
* PF deduction is 12% of basic salary.
* Professional tax is ₹200 per employee.

---

## Frontend Features

* Run Payroll
* View Payroll Summary
* View Employee Payslip
* Print Payslip
* Loading Indicators
* Validation Messages

---

## Unit Tests

Run tests:

```bash
dotnet test
```

Current test coverage includes:

* Invalid month validation
* Invalid year validation
* Successful payroll execution
* Payroll retrieval
* Payslip retrieval

---

## Future Improvements

* Authentication and Authorization
* Pagination
* PDF Payslip Generation
* Audit Logging
* Docker Support
* CI/CD Pipeline

---
