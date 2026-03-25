# Banking Transaction & Loan Management System

ASP.NET Core MVC project based on your photo requirements.

## Modules implemented
1. Customer Account & Profile Management
2. Transaction Processing (deposit, withdrawal, transfer)
3. Loan Application & Approval Workflow
4. Loan Repayment & Interest Calculation
5. Financial Reporting & Audit Logs

## Tech stack
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Simple Razor views

## Project structure
- Controllers
- Models
- Services
- Data/ApplicationDbContext.cs
- Views
- DatabaseSchema.sql

## Steps to run locally
1. Install .NET SDK 8 and SQL Server.
2. Create a database named `BankingTransactionLoanDb`.
3. Update the connection string in `appsettings.json`.
4. Run the SQL in `DatabaseSchema.sql` or use EF migrations.
5. Open terminal in the project folder.
6. Run:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```
7. Open the local URL shown in terminal.

## Suggested commands for EF migrations
```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Notes
- Interest calculation is simplified as per the requirement photo.
- Transactions are simulated; there is no real banking API integration.
- Authentication/roles are not added yet. You can extend this using ASP.NET Identity if needed.
