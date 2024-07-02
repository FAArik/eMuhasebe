using AutoMapper;
using eMuhasebeApi.Application.Features.Companies.CreateCompany;
using eMuhasebeApi.Application.Features.Companies.UpdateCompany;
using eMuhasebeApi.Application.Features.Users.CreateUser;
using eMuhasebeApi.Application.Features.Users.UpdateUser;
using eMuhasebeApi.Domain.Entities;

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
        }
    }
}
