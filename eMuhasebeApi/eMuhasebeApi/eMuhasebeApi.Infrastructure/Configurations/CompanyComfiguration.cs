using eMuhasebeApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMuhasebeApi.Infrastructure.Configurations;
public sealed class CompanyComfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");
        builder.Property(p => p.TaxNumber).HasColumnType("varchar(11)");
        builder.OwnsOne(x => x.Database, builder =>
        {
            builder.Property(prop => prop.Server).HasColumnName("Server");
            builder.Property(prop => prop.DatabaseName).HasColumnName("DatabaseName");
            builder.Property(prop => prop.UserId).HasColumnName("UserId");
            builder.Property(prop => prop.Password).HasColumnName("Password");
        });


        builder.HasQueryFilter(p => !p.isDeleted);
    }
}
