using Cms.Api.Models.ContetnItems;
using Cms.Application.Content.ContentItems.Commands.CreateContentItem;
using Cms.Application.Content.ContentItems.Commands.DeleteContentItem;
using Cms.Application.Content.ContentItems.Commands.PublishContentItem;
using Cms.Application.Content.ContentItems.Commands.RollbackContentItem;
using Cms.Application.Content.ContentItems.Commands.UpdateContentItem;
using Cms.Application.Content.ContentItems.Queries;
using Cms.Application.Content.ContentItems.Queries.GetContentItemByVersion;
using Cms.Application.Content.ContentItems.Queries.GetContentItemVersions;
using Cms.Application.Content.ContentItems.Queries.ListContentItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContentItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContentItemRequest request)
    {
        var command = new CreateContentItemCommand(
            request.ContentTypeId,
            request.Title,
            request.Slug,
            request.Values

            .Select(v => new ContentFieldValueDto(v.FieldName, v.Value))
            .ToList()
            );

        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAll(Guid id)
    {
        var res = await _mediator.Send(new GetByIdContentItemsQuery(id));
        return Ok(res);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContentItemRequest request)
    {
        var command = new UpdateContentItemCommand(
            id,
            request.Values.Select(v => new UpdateFieldValueDto(v.FieldName, v.Values)).ToList()
        );

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("{id:guid}/publish")]
    public async Task<IActionResult> Publish(Guid id)
    {
        var command = new PublishContentItemCommand(id);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet("{id:guid}/versions")]
    public async Task<IActionResult> GetVersions(Guid id)
    {
        var res = await _mediator.Send(new GetContentItemVersionsQuery(id));
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] Guid? contentTypeId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20
        )
    {
        var res = await _mediator.Send(new ListContentItemsQuery(
            contentTypeId,
            page,
            pageSize
            ));

        return Ok(res);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteContentItemCommand(id));
        return NoContent();
    }

    [HttpPost("{id:guid}/rollback/{versionNumber:int}")]
    public async Task<IActionResult> Rollback(Guid id, int versionNumber)
    {
        await _mediator.Send(new RollbackContentItemCommand(id, versionNumber));
        return Ok();
    }

    [HttpGet("{id:guid}/versions/{versionNumber:int}")]
    public async Task<IActionResult> GetVersion(Guid id, int versionNumber)
    {
        var res = await _mediator.Send(new GetContentItemByVersionQuery(id, versionNumber));

        return Ok(res);
    }

}