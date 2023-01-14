using Domain.Features.Queries.ChoreStatuses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Response.ChoreStatus.v1;
using Api.Dto.Request.ChoreStatus.v1;
using Domain.Features.Commands.ChoreStatus;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChoreStatusController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ChoreStatusController> _logger;

    public ChoreStatusController(IMediator mediator, IMapper mapper, ILogger<ChoreStatusController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }   

    [HttpGet]
    public async Task<ActionResult<IList<ChoreStatus>>> GetAllChoreStatuses()
    {
        try
        {
            var result = await _mediator.Send(new GetAllChoreStatusesQuery());
            return Ok(result);   
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in ChoreStatus controller: GetAllChoreStatuses");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetChoreStatusById/")]
    public async Task<ActionResult<IList<ChoreStatusResponseDto>>> GetChoreStatusById([FromQuery] GetChoreStatusByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetChoreStatusByIdRequestDto, GetChoreStatusByIdQuery>(request));
            return result.Count > 0 ? Ok(_mapper.Map<IList<ChoreStatusResponseDto>>(result)) : new List<ChoreStatusResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in ChoreStatus controller: GetChoreStatusById");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ChoreStatus>> PostChoreStatusAsync(PostChoreStatusRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostChoreStatusRequestDto, AddChoreStatusCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}