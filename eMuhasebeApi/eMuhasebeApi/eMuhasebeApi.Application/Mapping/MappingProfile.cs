using AutoMapper;
using eMuhasebeApi.Application.Features.Banks.CreateBank;
using eMuhasebeApi.Application.Features.Banks.UpdateBank;
using eMuhasebeApi.Application.Features.CashRegisters.CreateCashRegister;
using eMuhasebeApi.Application.Features.CashRegisters.UpdateCashRegister;
using eMuhasebeApi.Application.Features.Companies.CreateCompany;
using eMuhasebeApi.Application.Features.Companies.UpdateCompany;
using eMuhasebeApi.Application.Features.Customers.CreateCustomer;
using eMuhasebeApi.Application.Features.Customers.UpdateCustomer;
using eMuhasebeApi.Application.Features.Invoices.CreateInvoice;
using eMuhasebeApi.Application.Features.Products.CreateProduct;
using eMuhasebeApi.Application.Features.Products.UpdateProduct;
using eMuhasebeApi.Application.Features.Users.CreateUser;
using eMuhasebeApi.Application.Features.Users.UpdateUser;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Enums;

namespace eMuhasebeApi.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, AppUser>();
            CreateMap<UpdateUserCommand, AppUser>();

            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<UpdateCompanyCommand, Company>();

            CreateMap<CreateCashRegisterCommand, CashRegister>().ForMember(x => x.CurrencyType,
                options => options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue)));
            CreateMap<UpdateCashRegisterCommand, CashRegister>().ForMember(x => x.CurrencyType,
                options => options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue)));


            CreateMap<CreateBankCommand, Bank>().ForMember(x => x.CurrencyType,
                options => options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue)));
            CreateMap<UpdateBankCommand, Bank>().ForMember(x => x.CurrencyType,
                options => options.MapFrom(map => CurrencyTypeEnum.FromValue(map.CurrencyTypeValue)));

            CreateMap<CreateCustomerCommand, Customer>().ForMember(x => x.Type,
                options => options.MapFrom(map => CustomerTypeEnum.FromValue(map.TypeValue)));
            CreateMap<UpdateCustomerCommand, Customer>().ForMember(x => x.Type,
                options => options.MapFrom(map => CustomerTypeEnum.FromValue(map.TypeValue)));

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

            CreateMap<CreateInvoiceCommand, Invoice>()
                .ForMember(x => x.Type, opt => { opt.MapFrom(m => InvoiceTypeEnum.FromValue(m.TypeValue)); })
                .ForMember(x => x.Details, opt =>
                {
                    opt.MapFrom(m => m.Details.Select(s => new InvoiceDetail()
                    {
                        ProductId = s.ProductId,
                        Quantity = s.Quantity,
                        Price = s.Price,
                    }).ToList());
                }).ForMember(x => x.Amount,
                    opt => { opt.MapFrom(m => m.Details.Sum(s => s.Quantity * s.Price)); });
        }
    }
}