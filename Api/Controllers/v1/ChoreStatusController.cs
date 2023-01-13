using Domain.Features.Queries.ChoreStatuses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Response.ChoreStatus.v1;
using Api.Dto.Request.ChoreStatus.v1;

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
    public async Task<ActionResult<ChoreStatusResponseDto>> GetChoreStatusById([FromQuery]GetChoreStatusByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetChoreStatusByIdRequestDto, GetChoreStatusByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<ChoreStatusResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in ChoreStatus controller: GetChoreStatusById");
            return BadRequest(ex.Message);
        }
    }
}