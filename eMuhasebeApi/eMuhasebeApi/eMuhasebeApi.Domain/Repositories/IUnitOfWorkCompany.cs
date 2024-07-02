namespace eMuhasebeApi.Domain.Repositories
{
    public interface IUnitOfWorkCompany
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
