using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Enums;
using eMuhasebeApi.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eMuhasebeApi.Infrastructure.Context;

internal sealed class CompanyDbContext : DbContext, IUnitOfWorkCompany
{
    #region connection
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

    #endregion

    public DbSet<CashRegister> CashRegisters { get; set; }
    public DbSet<CashRegisterDetail> CashRegisterDetails { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<BankDetail> BankDetails { get; set; }
    public DbSet<Customer> Customers{ get; set; }
    public DbSet<CustomerDetail> CustomerDetails{ get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region cashRegister
        modelBuilder.Entity<CashRegister>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegister>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegister>().Property(p => p.CurrencyType).HasConversion(x => x.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder.Entity<CashRegister>().HasQueryFilter(x => !x.isDeleted);
        modelBuilder.Entity<CashRegister>().HasMany(x => x.Details).WithOne().HasForeignKey(x => x.CashRegisterId);
        #endregion

        #region cashRegisterDetail
        modelBuilder.Entity<CashRegisterDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CashRegisterDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        #endregion

        #region Bank
        modelBuilder.Entity<Bank>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<Bank>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<Bank>().Property(p => p.CurrencyType).HasConversion(x => x.Value, value => CurrencyTypeEnum.FromValue(value));
        modelBuilder.Entity<Bank>().HasQueryFilter(x => !x.isDeleted);
        modelBuilder.Entity<Bank>().HasMany(x => x.Details).WithOne().HasForeignKey(x => x.BankId);
        #endregion

        #region BankDetails
        modelBuilder.Entity<BankDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<BankDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        #endregion

        #region Customer
        modelBuilder.Entity<Customer>().Property(p => p.Type).HasConversion(x=>x.Value,val =>CustomerTypeEnum.FromValue(val));
        modelBuilder.Entity<Customer>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<Customer>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<Customer>().HasQueryFilter(x => !x.isDeleted);
        #endregion

        #region CustomerDetails
        modelBuilder.Entity<CustomerDetail>().Property(p => p.DepositAmount).HasColumnType("money");
        modelBuilder.Entity<CustomerDetail>().Property(p => p.WithdrawalAmount).HasColumnType("money");
        modelBuilder.Entity<CustomerDetail>().Property(p => p.Type).HasConversion(x=>x.Value,val =>CustomerDetailTypeEnum.FromValue(val));
        #endregion
    }

}
