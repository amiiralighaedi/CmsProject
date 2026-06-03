using Cms.Api.Models;
using Cms.Api.Models.Media;
using Cms.Application.Media.Commands.DeleteMedia;
using Cms.Application.Media.Commands.GetMedia;
using Cms.Application.Media.Commands.ListMedia;
using Cms.Application.Media.Commands.UploadMedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MediaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadMediaRequest request)
    {
        var id = await _mediator.Send(
            new UploadMediaCommands(request.File));

        return Ok(id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var media = await _mediator.Send(
            new GetMediaQuery(id));

        return Ok(media);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(
            new DeleteMediaCommand(id));

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var list = await _mediator.Send(
            new LIstMediaQuery());

        return Ok(list);
    }
}