using BankingTransactionLoanManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingTransactionLoanManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<TransactionRecord> Transactions => Set<TransactionRecord>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Repayment> Repayments => Set<Repayment>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(x => x.CustomerId);
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(100);
            entity.Property(x => x.ContactInfo).HasMaxLength(150);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(x => x.AccountId);
            entity.Property(x => x.AccountType).HasConversion<string>().HasMaxLength(20);
            entity.Property(x => x.Balance).HasColumnType("decimal(12,2)");
            entity.HasOne(x => x.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TransactionRecord>(entity =>
        {
            entity.ToTable("Transaction");
            entity.HasKey(x => x.TransactionId);
            entity.Property(x => x.TransactionType).HasConversion<string>().HasMaxLength(20);
            entity.Property(x => x.Amount).HasColumnType("decimal(12,2)");
            entity.HasOne(x => x.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(x => x.LoanId);
            entity.Property(x => x.LoanAmount).HasColumnType("decimal(12,2)");
            entity.Property(x => x.InterestRate).HasColumnType("decimal(5,2)");
            entity.Property(x => x.LoanStatus).HasConversion<string>().HasMaxLength(20);
            entity.HasOne(x => x.Customer)
                .WithMany(c => c.Loans)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Repayment>(entity =>
        {
            entity.HasKey(x => x.RepaymentId);
            entity.Property(x => x.AmountPaid).HasColumnType("decimal(12,2)");
            entity.Property(x => x.BalanceRemaining).HasColumnType("decimal(12,2)");
            entity.HasOne(x => x.Loan)
                .WithMany(l => l.Repayments)
                .HasForeignKey(x => x.LoanId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(x => x.LogId);
            entity.Property(x => x.ActionPerformed).HasMaxLength(100);
            entity.Property(x => x.PerformedBy).HasMaxLength(100);
            entity.HasOne(x => x.Transaction)
                .WithMany(t => t.AuditLogs)
                .HasForeignKey(x => x.TransactionId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
