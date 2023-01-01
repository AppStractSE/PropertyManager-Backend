using Domain.Features.Queries.Teams;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class TeamController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TeamController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Team>>> GetAllTeams()
    {
        var result = await _mediator.Send(new GetAllTeamsQuery());
        return Ok(result);
    }
}