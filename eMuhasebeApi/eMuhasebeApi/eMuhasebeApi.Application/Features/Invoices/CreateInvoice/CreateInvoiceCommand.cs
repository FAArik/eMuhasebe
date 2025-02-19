using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Dtos;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Enums;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Invoices.CreateInvoice;

public sealed record CreateInvoiceCommand(
    int TypeValue,
    DateOnly Date,
    string InvoiceNumber,
    Guid CustomerId,
    List<InvoiceDetailDto> InvoiceDetails
) : IRequest<Result<string>>;

internal sealed class CreateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IProductRepository productRepository,
    IProductDetailRepository productDetailRepository,
    ICustomerRepository customerRepository,
    ICustomerDetailRepository customerDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService,
    IMapper mapper
) : IRequestHandler<CreateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        #region Invoice and Details

        Invoice invoice = mapper.Map<Invoice>(request);

        await invoiceRepository.AddAsync(invoice, cancellationToken);

        #endregion

        #region Customer

        Customer? customer =
            await customerRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.CustomerId,
                cancellationToken);

        if (customer is null)
        {
            return Result<string>.Failure("Müşteri bulunamadı");
        }

        customer.DepositAmount += request.TypeValue == 2 ? invoice.Amount : 0;
        customer.WithdrawalAmount += request.TypeValue == 1 ? invoice.Amount : 0;

        CustomerDetail customerDetail = new()
        {
            CustomerId = customer.Id,
            Date = request.Date,
            DepositAmount = request.TypeValue == 2 ? invoice.Amount : 0,
            WithdrawalAmount = request.TypeValue == 1 ? invoice.Amount : 0,
            Description = invoice.InvoiceNumber + " Numaralı " + invoice.Type.Name,
            Type = request.TypeValue == 1
                ? CustomerDetailTypeEnum.PurchaseInvoice
                : CustomerDetailTypeEnum.SellingInvoice,
            InvoiceId = invoice.Id
        };
        await customerDetailRepository.AddAsync(customerDetail, cancellationToken);

        #endregion

        #region Product

        foreach (var detail in request.InvoiceDetails)
        {
            Product product =
                await productRepository.GetByExpressionWithTrackingAsync(x => x.Id == detail.ProductId,
                    cancellationToken);

            product.Deposit += request.TypeValue == 1 ? detail.Quantity : 0;
            product.Withdrawal += request.TypeValue == 2 ? detail.Quantity : 0;

            ProductDetail productDetail = new()
            {
                ProductId = product.Id,
                Date = request.Date,
                Description = invoice.InvoiceNumber + " Numaralı " + invoice.Type.Name,
                Deposit = request.TypeValue == 1 ? detail.Quantity : 0,
                Withdrawal = request.TypeValue == 2 ? detail.Quantity : 0,
                InvoiceId = invoice.Id
            };
            await productRepository.AddAsync(product, cancellationToken);
            await productDetailRepository.AddAsync(productDetail, cancellationToken);
        }

        #endregion

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("sellingInvoices");
        cacheService.Remove("purchaseInvoices");
        cacheService.Remove("customers");
        cacheService.Remove("products");
        return invoice.Type.Name + " kaydı başarıyla tamamlandı";
    }
}