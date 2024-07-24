using eMuhasebeApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMuhasebeApi.Infrastructure.Configurations;

internal sealed class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
    public void Configure(EntityTypeBuilder<CompanyUser> builder)
    {
        builder.HasKey(x => new { x.AppUserId, x.CompanyId });
        builder.HasQueryFilter(filter => !filter.Company!.isDeleted);
    }
}
