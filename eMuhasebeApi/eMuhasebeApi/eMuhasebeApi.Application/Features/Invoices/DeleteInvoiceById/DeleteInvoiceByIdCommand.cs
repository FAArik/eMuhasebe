using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Invoices.DeleteInvoiceById;

public sealed record DeleteInvoiceByIdCommand(
    Guid Id
) : IRequest<Result<string>>;

internal sealed class DeleteInvoiceByIdCommandHandler(
    IInvoiceRepository invoiceRepository,
    ICustomerRepository customerRepository,
    ICustomerDetailRepository customerDetailRepository,
    IProductRepository productRepository,
    IProductDetailRepository productDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
) : IRequestHandler<DeleteInvoiceByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteInvoiceByIdCommand request, CancellationToken cancellationToken)
    {
        Invoice? invoice = await invoiceRepository.Where(x => x.Id == request.Id).Include(x => x.Details)
            .FirstOrDefaultAsync(cancellationToken);

        if (invoice is null)
        {
            return Result<string>.Failure("Fatura Bulunamadı");
        }

        CustomerDetail? customerDetail = await customerDetailRepository.Where(x => x.InvoiceId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (customerDetail is not null)
        {
            customerDetailRepository.Delete(customerDetail);
        }

        Customer? customer = await customerRepository.Where(x => x.Id == invoice.CustomerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is not null)
        {
            customer.DepositAmount -= invoice.Type.Value == 1 ? 0 : invoice.Amount;
            customer.WithdrawalAmount -= invoice.Type.Value == 2 ? 0 : invoice.Amount;
            customerRepository.Update(customer);
        }

        List<ProductDetail> productDetails = await productDetailRepository.Where(x => x.InvoiceId == invoice.Id)
            .ToListAsync(cancellationToken);

        foreach (var detail in productDetails)
        {
            Product? product =
                await productRepository.GetByExpressionWithTrackingAsync(x => x.Id == detail.ProductId,
                    cancellationToken);
            if (product is not null)
            {
                product.Deposit -= detail.Deposit;
                product.Withdrawal -= detail.Withdrawal;
                productRepository.Update(product);
            }
        }

        productDetailRepository.DeleteRange(productDetails);
        invoiceRepository.Delete(invoice);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("invoices");
        cacheService.Remove("customers");
        cacheService.Remove("products");

        return invoice.Type.Name + " Kaydı başarıyla silindi";
    }
}