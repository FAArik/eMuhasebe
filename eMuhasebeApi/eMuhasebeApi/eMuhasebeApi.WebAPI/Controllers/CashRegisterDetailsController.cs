using eMuhasebeApi.Application.Features.CashRegisterDetails.CreateCashRegisterDetail;
using eMuhasebeApi.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById;
using eMuhasebeApi.Application.Features.CashRegisterDetails.GetAllCashRegisterDetailsQuery;
using eMuhasebeApi.Application.Features.CashRegisterDetails.UpdateCashRegisterDetail;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class CashRegisterDetailsController : ApiController
    {
        public CashRegisterDetailsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost] 
        public async Task<IActionResult> GetAll(GetAllCashRegisterDetailsQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCashRegisterDetailCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCashRegisterDetailCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteCashRegisterDetailByIdCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
