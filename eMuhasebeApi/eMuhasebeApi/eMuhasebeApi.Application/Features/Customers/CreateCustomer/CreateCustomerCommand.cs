using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Nmme,
    int TypeValue,
    string City,
    string Town,
    string FullAddress,
    string TaxDepartment,
    string TaxNumber
    ):IRequest<Result<string>>;
