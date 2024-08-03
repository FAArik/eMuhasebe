using AutoMapper;
using eMuhasebeApi.Application.Features.CashRegisters.CreateCashRegister;
using eMuhasebeApi.Application.Features.CashRegisters.UpdateCashRegister;
using eMuhasebeApi.Application.Features.Companies.CreateCompany;
using eMuhasebeApi.Application.Features.Companies.UpdateCompany;
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
            CreateMap<UpdateUserCommand,AppUser>();

            CreateMap<CreateCompanyCommand,Company>();
            CreateMap<UpdateCompanyCommand,Company>();

            CreateMap<CreateCashRegisterCommand, CashRegister>().ForMember(x => x.CurrencyType, options => options.MapFrom(map => CurrencyTypeEnum.FromValue(map.TypeValue)));
            CreateMap<UpdateCashRegisterCommand, CashRegister>().ForMember(x => x.CurrencyType, options => options.MapFrom(map => CurrencyTypeEnum.FromValue(map.TypeValue)));

        }
    }
}
