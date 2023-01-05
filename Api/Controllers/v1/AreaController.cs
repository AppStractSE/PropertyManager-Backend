using Domain.Features.Queries.Areas;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Domain.Features.Commands.Area;
using Api.Dto.Request.Area.v1;
using Api.Dto.Response.Area.v1;

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

    [HttpGet]
    [Route("GetAreaById/")]
    public async Task<ActionResult<AreaResponseDto>> GetAreaById([FromQuery] GetAreaByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetAreaByIdRequestDto, GetAreaByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<AreaResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Area>> PostAreaAsync(PostAreaRequestDto request)
    {
        var result = await _mediator.Send(_mapper.Map<PostAreaRequestDto, AddAreaCommand>(request));
        return Ok(result);
    }
    
    [HttpPatch]
    public async Task<ActionResult<Area>> PatchAreaAsync(PatchAreaRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PatchAreaRequestDto, UpdateAreaCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}