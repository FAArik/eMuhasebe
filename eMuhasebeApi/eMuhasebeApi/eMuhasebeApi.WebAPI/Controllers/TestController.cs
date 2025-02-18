using eMuhasebeApi.WebAPI.Abstractions;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeApi.WebAPI.Controllers;

public class TestController : ApiController
{
    private readonly IFluentEmail _fluentEmail;

    public TestController(IMediator mediator, IFluentEmail fluentEmail) : base(mediator)
    {
        _fluentEmail = fluentEmail;
    }

    [HttpGet]
    public async Task<IActionResult> SendTestEmail()
    {
        await _fluentEmail.To("faarik132@gmail.com").Subject("Test Maili").Body("<h1>Test maili</h1>", true)
            .SendAsync();
        return NoContent();
    }
}