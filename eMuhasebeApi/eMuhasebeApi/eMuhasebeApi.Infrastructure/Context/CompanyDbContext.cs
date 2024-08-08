using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Enums;
using eMuhasebeApi.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eMuhasebeApi.Infrastructure.Context;

internal sealed class CompanyDbContext : DbContext, IUnitOfWorkCompany
{
    private string connectionString = string.Empty;
    public CompanyDbContext(Company company)
    {
        CreateConnectionStringWithCompany(company);
    }
    public CompanyDbContext(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        CreateConnectionString(httpContextAccessor, context);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
    public DbSet<CashRegister> CashRegisters { get; set; }
    public DbSet<CashRegisterDetail> CashRegisterDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CashRegister>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegister>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegister>().Property(p => p.BalanceAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegister>().Property(p => p.CurrencyType).HasConversion(x => x.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder.Entity<CashRegister>().HasQueryFilter(x => !x.isDeleted);
        modelBuilder.Entity<CashRegister>().HasMany(x => x.Details).WithOne().HasForeignKey(x => x.CashRegisterId);

        modelBuilder.Entity<CashRegisterDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegisterDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegisterDetail>().HasQueryFilter(x => !x.isDeleted);

    }
    private void CreateConnectionString(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        if (httpContextAccessor.HttpContext is null) return;
        string? companyId = httpContextAccessor.HttpContext.User.FindFirstValue("CompanyId");

        if (string.IsNullOrEmpty(companyId)) return;
        Company? company = context.Companies.Find(Guid.Parse(companyId));
        if (company is null) return;

        CreateConnectionStringWithCompany(company);
    }
    private void CreateConnectionStringWithCompany(Company company)
    {
        if (string.IsNullOrEmpty(company.Database.UserId))
        {
            connectionString =
                $"Data Source={company.Database.Server};" +
                $"Initial Catalog={company.Database.DatabaseName};" +
                $"Persist Security Info=False;" +
                $"Integrated Security=True;" +
                $"MultipleActiveResultSets=False;" +
                $"Encrypt=True;" +
                $"TrustServerCertificate=False;" +
                $"Connection Timeout=30;";
        }
        else
        {
            connectionString =
                $"Data Source={company.Database.Server};" +
                $"Initial Catalog={company.Database.DatabaseName};" +
                $"Persist Security Info=False;" +
                $"User ID={company.Database.UserId};" +
                $"Password={company.Database.Password};" +
                $"MultipleActiveResultSets=False;" +
                $"Encrypt=True;" +
                $"TrustServerCertificate=True;" +
                $"Connection Timeout=30;";
        }
    }
}
