using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

public class IInvoiceDetailRepository : Repository<InvoiceDetail, CompanyDbContext>,
    Domain.Repositories.IInvoiceDetailRepository
{
    public IInvoiceDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}