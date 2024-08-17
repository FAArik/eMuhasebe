using eMuhasebeApi.Application.Features.BankDetails.GetAllBankDetails;
using eMuhasebeApi.Application.Features.BankDetails.CreateBankDetail;
using eMuhasebeApi.Application.Features.BankDetails.DeleteBankDetailById;
using eMuhasebeApi.Application.Features.BankDetails.UpdateBankDetail;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class BankDetailsController : ApiController
    {
        public BankDetailsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost] 
        public async Task<IActionResult> GetAll(GetAllBankDetailsQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBankDetailCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBankDetailCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteBankDetailByIdCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
