using eMuhasebeApi.Application.Features.ProductDetails.GetAllProductDetails;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers;

public class ProductDetailsController : ApiController
{
    public ProductDetailsController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllProductDetailsQuery query, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return StatusCode(response.StatusCode, response);
    } 
}
