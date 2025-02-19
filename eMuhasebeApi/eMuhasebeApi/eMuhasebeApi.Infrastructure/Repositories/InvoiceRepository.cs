using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

public class InvoiceRepository : Repository<Invoice, CompanyDbContext>, IInvoiceRepository
{
    public InvoiceRepository(CompanyDbContext context) : base(context)
    {
    }
}