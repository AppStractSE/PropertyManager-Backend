using Domain.Features.Queries.ChoreComments;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChoreCommentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChoreCommentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IList<ChoreComment>>> GetAllChoreComments()
    {
        var result = await _mediator.Send(new GetAllChoreCommentsQuery());
        return Ok(result);
    }
}