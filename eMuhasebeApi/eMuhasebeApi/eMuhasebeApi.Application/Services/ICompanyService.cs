using eMuhasebeApi.Domain.Entities;

namespace eMuhasebeApi.Application.Services;

public interface ICompanyService
{
    void MigrateAll(List<Company> companies);
}