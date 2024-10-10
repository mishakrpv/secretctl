using AccessManagement.Application.Commands.CreateCredentials;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers;

public class ProjectController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("credentials")]
    public async Task<IActionResult> CreateCredentials([FromBody] CreateCredentialsRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}