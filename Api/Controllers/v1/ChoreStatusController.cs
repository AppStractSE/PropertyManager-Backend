using Domain.Features.Queries.Chores;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Domain.Features.Queries.ChoreStatuses;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChoreStatusController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChoreStatusController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IList<ChoreStatus>>> GetAllChoreStatuses()
    {
        var result = await _mediator.Send(new GetAllChoreStatusesQuery());
        return Ok(result);
    }
}