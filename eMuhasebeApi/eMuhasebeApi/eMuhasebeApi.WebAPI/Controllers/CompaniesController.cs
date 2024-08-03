using eMuhasebeApi.Application.Features.Companies.CreateCompany;
using eMuhasebeApi.Application.Features.Companies.DeleteCompanyById;
using eMuhasebeApi.Application.Features.Companies.GetAllCompanies;
using eMuhasebeApi.Application.Features.Companies.MigrateAllCompanies;
using eMuhasebeApi.Application.Features.Companies.UpdateCompany;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers;

public sealed class CompaniesController : ApiController
{
    public CompaniesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCompaniesQuery query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCompany(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCompanyById(DeleteCompanyByIdCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    public async Task<IActionResult> MigrateAll(CancellationToken cancellationToken)
    {
        MigrateAllCompaniesCommand command = new();
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
