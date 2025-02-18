using eMuhasebeApi.Application.Features.CustomerDetails.GetAllCustomerDetails;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers;

public class CustomerDetailsController : ApiController
{
    public CustomerDetailsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllCustomerDetailsQuery query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    } 
}
