using Cms.Application.Content.Interfaces;
using Cms.Application.Content.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IContentQueryService _contentQueryService;

    public QueryController(IMediator mediator, IContentQueryService contentQueryService)
    {
        _mediator = mediator;
        _contentQueryService = contentQueryService;
    }

    [HttpPost("{contentType}")]
    public async Task<IActionResult> Query(
        string contentType,
        [FromBody] QueryRequest request
        )
    {
        var res = await _contentQueryService.QueryAsync(contentType, request);
        return Ok(res);
    }
}
