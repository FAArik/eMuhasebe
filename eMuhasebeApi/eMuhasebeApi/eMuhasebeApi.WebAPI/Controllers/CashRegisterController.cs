using eMuhasebeApi.Application.Features.CashRegisters.CreateCashRegister;
using eMuhasebeApi.Application.Features.CashRegisters.DeleteCashRegisterById;
using eMuhasebeApi.Application.Features.CashRegisters.GetAllCashRegisters;
using eMuhasebeApi.Application.Features.CashRegisters.UpdateCashRegister;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers;

public sealed class CashRegisterController : ApiController
{
    public CashRegisterController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCashRegistersQuery query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCashRegister(CreateCashRegisterCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCashRegister(UpdateCashRegisterCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCashRegisterById(DeleteCashRequestByIdCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}