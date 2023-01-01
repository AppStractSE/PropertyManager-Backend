using Domain.Features.Queries.Areas;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class AreaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AreaController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Area>>> GetAllAreas()
    {
        var result = await _mediator.Send(new GetAllAreasQuery());
        return Ok(result);
    }
}