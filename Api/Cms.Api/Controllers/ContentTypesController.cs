using Cms.Api.Models.AddField;
using Cms.Api.Models.ContentTypes;
using Cms.Application.Content.ContentTypes.Commands.AddFieldContentType;
using Cms.Application.Content.ContentTypes.Commands.CreateContentType;
using Cms.Application.Content.ContentTypes.Queries.GetContentTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] // همه باید لاگین باشند
public class ContentTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContentTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateContentTypeRequest request)
    {
        var command = new CreateContentTypeCommand(
            request.Name,
            request.Slug,
            request.Description
        );

        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _mediator.Send(new GetContentTypesQuery());
        return Ok(res);
    }

    [HttpPost("{id:guid}/fields")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddField(Guid id, [FromBody] AddFieldRequest request)
    {
        var command = new AddFieldToContentTypeCommand(
            id,
            request.Name,
            request.Type,
            request.IsRequired
        );

        await _mediator.Send(command);
        return Ok();
    }
}
