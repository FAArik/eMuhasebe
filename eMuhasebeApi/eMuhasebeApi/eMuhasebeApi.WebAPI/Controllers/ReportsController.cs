using eMuhasebeApi.Application.Features.Reports.ProductProfitabilityReports;
using eMuhasebeApi.Application.Features.Reports.PurchaseReports;
using eMuhasebeApi.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers;

public class ReportsController : ApiController
{
    public ReportsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> ProductProfitabilityReports(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ProductProfitabilityReportsQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    public async Task<IActionResult> PurchaseReport(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new PurchaseReportQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}