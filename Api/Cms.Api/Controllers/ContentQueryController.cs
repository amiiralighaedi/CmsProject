using Cms.Application.Content.Queries.GetContentItem;
using Cms.Application.Content.Queries.GetContentList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContentQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{contentType}")]
        public async Task<IActionResult> GetList(
            string contentType,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20
            )
        {
            var res = await _mediator.Send(new GetContentListQuery(contentType, page, pageSize));
            return Ok(res);
        }

        [HttpGet("{contentType}/{id:guid}")]
        public async Task<IActionResult> GetById(
            string contentType, Guid id
            )
        {
            var res = await _mediator.Send(new GetContentItemQuery(contentType, Id: id));
            if (res is null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("{contentType}/slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string contentType, string slug)
        {
            var res = await _mediator.Send(new GetContentItemQuery(contentType, Slug: slug));
            if (res is null)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
